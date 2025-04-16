using Identity.Api.Entities;
using Identity.Api.Services.Abstractions;
using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json;
using System.Reflection;
using Shared.Service;
using ILogger = Serilog.ILogger;
using Shared.Service.Abstractions;
using FluentValidation;
using Identity.Api.Repositories.Abstractions;
using static Shared.Dtos.Identity.SeedDtos;
using Microsoft.EntityFrameworkCore;
using Identity.Api.DependencyInjection.Extensions.Helpers;
using Identity.Api.DependencyInjection.Extensions.Mappings;
using Shared.Dtos.Extensions;

namespace Identity.Api.Services;

public class SeedService(ILogger logger, IUserService userService, IValidatorFactory validatorFactory, IRepositoryManager repoManager) 
    : BaseService<IRepositoryManager>(logger, repoManager), ISeedService
{
    //private readonly IRegisterService _registerService;
    private readonly IUserService _userService = userService;
    private readonly IValidatorFactory _validatorFactory = validatorFactory;

    public async Task SeedDataAsync(SeedDataRequest request)
    {
        if (request.IsReset)
        {
            try
            {
                await _RemoveCurrentSecurityAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }

        if (request.IsSeed)
        {
            await _ReSeedSecurityAsync();
        }
    }

    private async Task _ReSeedSecurityAsync()
    {
        _repoManager.SetAuditLog(false);

        var operationRequests = await _ReadSeedJsonFileAsync<OperationRequest>("operations");
        var permissionRequests = await _ReadSeedJsonFileAsync<PermissionRequest>("permissions");
        var roleRequests = await _ReadSeedJsonFileAsync<RoleRequest>("roles");
        var scopeRequests = await _ReadSeedJsonFileAsync<ScopeRequest>("scopes");

        var operations = await _CreateOrUpdateOperationAsync(operationRequests);
        var scopes = await _CreateOrUpdateScopeAsync(scopeRequests);
        var permissions = await _CreateOrUpdatePermissionAsync(operations, scopes);
        var roles = await _CreateOrUpdateRoleAsync(roleRequests);
        await _repoManager.SaveAsync();
        _repoManager.DetachEntities();

        await _AddNewAccessRuleAsync(roles, permissions);
        await _repoManager.SaveAsync();

        LogSeed(operations.Select(x => x.Code).Distinct().ToList()
            , scopes.Select(x => x.Code).Distinct().ToList()
            , permissions.Select(x => x.GetPermissionCode()).Distinct().ToList()
            , roles.Select(x => x.Code).Distinct().ToList());
    }

    private void LogSeed(List<string> operationCodes
        , List<string> scopeCodes
        , List<string> permissionCodes
        , List<string> roleCodes)
    {
        _logger.Information("=====================Add Operation=====================");
        operationCodes.ForEach(x => _logger.Information($"Operation {x}"));

        _logger.Information("=====================Add Scope=====================");
        scopeCodes.ForEach(x => _logger.Information($"Scope {x}"));

        _logger.Information("=====================Add Permission=====================");
        permissionCodes.ForEach(x => _logger.Information($"Permission {x}"));

        _logger.Information("=====================Add Role=====================");
        roleCodes.ForEach(x => _logger.Information($"Role {x}"));
    }

    private async Task _RemoveCurrentSecurityAsync()
    {
        await _repoManager.TruncateAsync(nameof(CustomIdentityDbContext.AccessRules));
        await _repoManager.TruncateAsync(nameof(CustomIdentityDbContext.Scopes));
        await _repoManager.TruncateAsync(nameof(CustomIdentityDbContext.Operations));
        await _repoManager.TruncateAsync(nameof(CustomIdentityDbContext.Permissions));
        await _repoManager.TruncateAsync(nameof(CustomIdentityDbContext.Roles));
    }

    private async Task<List<T>> _ReadSeedJsonFileAsync<T>(string fileName)
    {
        var result = new List<T>();
        var rootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        var fullPath = Path.Combine(rootPath, $"Seeds/{fileName}.json");
        using (StreamReader r = new StreamReader(fullPath))
        {
            string json = await r.ReadToEndAsync();
            result = JsonConvert.DeserializeObject<List<T>>(json);
        }

        return result;
    }

    private async Task<List<Scope>> _CreateOrUpdateScopeAsync(List<ScopeRequest> requests)
    {
        var newRequest = new List<ScopeRequest>(requests);
        var existingScopes = await _repoManager.Scope.FindAll().ToListAsync();

        //Case delete
        var scopeNeedToDelete = existingScopes.Where(x => !newRequest.Select(y => y.Code).Distinct().Contains(x.Code))
            .ToList();

        _repoManager.Scope.RemoveRange(scopeNeedToDelete);
        existingScopes.RemoveAll(x => scopeNeedToDelete.Select(y => y.Code).Distinct().Contains(x.Code));

        //Case update
        foreach (var existingScope in existingScopes)
        {
            var request = newRequest.FirstOrDefault(x => x.Code == existingScope.Code);
            if (request == null) continue;

            existingScope.Code = request.Code;
            existingScope.Name = request.Name;
            existingScope.UpdatedAt = DateTime.UtcNow;
            existingScope.UpdatedBy = "seed";
        }

        //Case create
        newRequest.RemoveAll(x => existingScopes.Select(y => y.Code).Contains(x.Code));
        var scopeNeedToCreate = newRequest.ToScopeList();
        _repoManager.Scope.AddRange(scopeNeedToCreate);

        return existingScopes.Union(scopeNeedToCreate).ToList();
    }

    private async Task<List<Operation>> _CreateOrUpdateOperationAsync(List<OperationRequest> requests)
    {
        var newRequest = new List<OperationRequest>(requests);
        var existingOperations = await _repoManager.Operation.FindAll().ToListAsync();

        //Case delete
        var operationNeedToDelete = existingOperations.Where(x => !newRequest.Select(y => y.Code).Distinct().Contains(x.Code)).ToList();
        _repoManager.Operation.RemoveRange(operationNeedToDelete);
        existingOperations.RemoveAll(x => operationNeedToDelete.Select(y => y.Code).Distinct().Contains(x.Code));

        //Case update
        foreach (var existingOperation in existingOperations)
        {
            var request = newRequest.FirstOrDefault(x => x.Code == existingOperation.Code);
            if (request == null) continue;

            existingOperation.Code = request.Code;
            existingOperation.Name = request.Name;
            existingOperation.UpdatedAt = DateTime.UtcNow;
            existingOperation.UpdatedBy = "seed";
        }

        //Case create
        newRequest.RemoveAll(x => existingOperations.Select(y => y.Code).Contains(x.Code));
        var operationNeedToCreate = newRequest.ToOperationList();
        _repoManager.Operation.AddRange(operationNeedToCreate);

        return existingOperations.Union(operationNeedToCreate).ToList();
    }

    private async Task<List<Permission>> _CreateOrUpdatePermissionAsync(
        List<Operation> operations,
        List<Scope> scopes)
    {
        var newRequests = (from op in operations
                           from sc in scopes
                           select new PermissionSeedRequest
                           {
                               OperationCode = op.Code,
                               ScopeCode = sc.Code,
                           }).ToList();

        var existingPermissions = await _repoManager.Permission.FindAll()
            .Include(x => x.Operation)
            .Include(x => x.Scope)
            .ToListAsync();

        //Case delete
        var permissionNeedToDelete = existingPermissions
            .Where(x =>
                !operations.Select(y => y.Code).Contains(x.Operation.Code)
                || !scopes.Select(y => y.Code).Contains(x.Scope.Code))
            .ToList();

        _repoManager.Permission.RemoveRange(permissionNeedToDelete);
        existingPermissions.RemoveAll(x => permissionNeedToDelete.Any(y => x.Id == y.Id));

        //Case create
        newRequests.RemoveAll(x => existingPermissions.Any(y => y.Operation.Code.Equals(x.OperationCode))
            && existingPermissions.Any(y => y.Scope.Code.Equals(x.ScopeCode)));

        var permissionsNeedToCreate = new List<Permission>();

        foreach (var newRequest in newRequests)
        {
            var operation = operations.FirstOrDefault(x => newRequest.OperationCode.Equals(x.Code));
            if (operation is null) continue;

            var scope = scopes.FirstOrDefault(x => newRequest.ScopeCode.Equals(x.Code));
            if (scope is null) continue;

            Permission permissionNeedToCreate = new()
            {
                Operation = operation,
                Scope = scope,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "seed",
            };
            permissionsNeedToCreate.Add(permissionNeedToCreate);
            _repoManager.Permission.Add(permissionNeedToCreate);
        }

        return existingPermissions.Union(permissionsNeedToCreate).ToList();
    }

    private async Task<List<Role>> _CreateOrUpdateRoleAsync(List<RoleRequest> requests)
    {
        var newRequests = new List<RoleRequest>(requests);
        var existingRoles = await _repoManager.Role.FindAll().ToListAsync();

        //Case delete
        var roleNeedToDelete = existingRoles.Where(x => !newRequests.Select(y => y.Code).Distinct().Contains(x.Code)).ToList();
        _repoManager.Role.RemoveRange(roleNeedToDelete);
        existingRoles.RemoveAll(x => roleNeedToDelete.Select(y => y.Code).Distinct().Contains(x.Code));

        //Case update
        foreach (var existingRole in existingRoles)
        {
            var request = newRequests.FirstOrDefault(x => x.Code == existingRole.Code);
            if (request == null) continue;

            existingRole.Code = request.Code;
            existingRole.Name = request.Name;
            existingRole.IsLocked = request.IsLocked;
            existingRole.Weight = request.Weight;
            existingRole.NormalizedName = request.NormalizedName;
            existingRole.UpdatedAt = DateTime.UtcNow;
            existingRole.UpdatedBy = "seed";
        }

        //Case create
        newRequests.RemoveAll(x => existingRoles.Select(y => y.Code).Contains(x.Code));
        var roleNeedToCreate = newRequests.ToRoleList();
        _repoManager.Role.AddRange(roleNeedToCreate);

        return existingRoles.Union(roleNeedToCreate).ToList();
    }

    private async Task _AddNewAccessRuleAsync(List<Role> roles, List<Permission> permissions)
    {
        var accessRules = (from p in permissions
                           from r in roles

                           select new AccessRule
                           {
                               PermissionId = p.Id,
                               RoleId = r.Id,
                           });

        var existingAccessRules = await _repoManager.AccessRule.FindAll().ToListAsync();

        existingAccessRules = existingAccessRules.Where(x =>
            accessRules.Any(y => x.RoleId == y.RoleId && x.PermissionId == y.PermissionId))
            .ToList();

        if (!existingAccessRules.IsNullOrEmpty())
        {
            var accRulesNeedToUpdate = accessRules.Where(x => existingAccessRules.Any(
                y => x.RoleId == y.RoleId && x.PermissionId == y.PermissionId))
                .ToList();

            foreach (var existingAccessRule in existingAccessRules)
            {
                var accRuleNeedToUpdate = accRulesNeedToUpdate.FirstOrDefault(x =>
                    x.RoleId == existingAccessRule.RoleId &&
                    x.PermissionId == existingAccessRule.PermissionId);

                existingAccessRule.Mode = accRuleNeedToUpdate?.Mode ?? false;
                existingAccessRule.PermissionId = accRuleNeedToUpdate?.PermissionId;
                existingAccessRule.RoleId = accRuleNeedToUpdate?.RoleId;
                existingAccessRule.UpdatedAt = DateTime.UtcNow;
                existingAccessRule.UpdatedBy = "seed";
            }

            _repoManager.AccessRule.UpdateRange(existingAccessRules);
        }

        var newAccessRules = accessRules.ToList();
        newAccessRules.RemoveAll(x => existingAccessRules.Any(y =>
            x.RoleId == y.RoleId &&
            x.PermissionId == y.PermissionId));

        foreach (var existingAccessRule in newAccessRules)
        {
            existingAccessRule.CreatedAt = DateTime.UtcNow;
            existingAccessRule.CreatedBy = "seed";
        }

        _repoManager.AccessRule.AddRange(newAccessRules);
    }
}