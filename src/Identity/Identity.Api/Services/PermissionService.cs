﻿using FilteringAndSortingExpression.Extensions;
using FluentValidation;
using Identity.Api.DependencyInjection.Extensions.Helpers;
using Identity.Api.DependencyInjection.Extensions.Mappings;
using Identity.Api.Exceptions;
using Identity.Api.Repositories.Abstractions;
using Identity.Api.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.Service;
using static Shared.Dtos.Identity.PermissionDtos;
using ILogger = Serilog.ILogger;

namespace Identity.Api.Services;

public class PermissionService : BaseService<IRepositoryManager>, IPermissionService
{
    private readonly IValidatorFactory _validatorFactory;

    public PermissionService(ILogger logger, IRepositoryManager repoManager, IValidatorFactory validatorFactory)
        : base(logger, repoManager)
    {
        _validatorFactory = validatorFactory;
    }

    public async Task<PermissionResponse> GetAsync(int id)
    {
        var result = await _repoManager.Permission.FindByCondition(x => x.Id == id)
            .Select(x => new PermissionResponse
            {
                Id = x.Id,
                Code = x.GetPermissionCode(),
                Name = x.GetPermissionName(),
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy
            })
            .FirstOrDefaultAsync()
            ?? throw new PermissionException.NotFound(id);

        return result;
    }

    public async Task<IEnumerable<PermissionResponse>> GetListAsync(PermissionListRequest request)
    {
        var result = await _repoManager.Permission.FindAll()
            .Select(x => x.ToPermissionResponse())
            .Filter(request)
            .ToListAsync();

        return result;
    }

    public async Task<List<PermissionGrpListByRoleResponse>> GetPermissionByRoleIdAsync(PermissionListByRoleRequest request)
    {
        var permissionsByRole = await (from p in _repoManager.Permission.FindAll()
                            .Include(x => x.Scope)
                            .Include(x => x.Operation)

                                       join ac in _repoManager.AccessRule.FindAll()
                                       on p.Id equals ac.PermissionId

                                       join r in _repoManager.Role.FindAll()
                                       on ac.RoleId equals r.Id

                                       select new PermissionListByRoleResponse()
                                       {
                                           ScopeId = p.Scope.Id,
                                           IsEnabled = ac.Mode,
                                           ScopeName = p.Scope.Name,
                                           OperationId = p.Operation.Id,
                                           RoleId = ac.RoleId ?? 0,
                                           OperationName = p.Operation.Name,
                                           PermissionName = p.GetPermissionName(),
                                           CreatedAt = ac.CreatedAt,
                                           UpdatedAt = ac.UpdatedAt,
                                           CreatedBy = ac.CreatedBy,
                                           UpdatedBy = ac.UpdatedBy
                                       })
                 .Filter(request)
                 .ToListAsync();

        var result = new List<PermissionGrpListByRoleResponse>();

        if (permissionsByRole.IsNullOrEmpty())
        {
            return result;
        }

        result = permissionsByRole
            .GroupBy(x => new { x.ScopeId, x.RoleId })
            .Select(x => new PermissionGrpListByRoleResponse
            {
                Id = x.Key.ScopeId,
                Name = x.FirstOrDefault()?.ScopeName ?? string.Empty,
                IsEnabled = x.Any(x => x.IsEnabled == true),
                PermissionName = x.FirstOrDefault()?.PermissionName,
                CreatedAt = x.OrderByDescending(x => x.UpdatedAt).FirstOrDefault()?.CreatedAt,
                UpdatedAt = x.OrderByDescending(x => x.UpdatedAt).FirstOrDefault()?.UpdatedAt,
                CreatedBy = x.OrderByDescending(x => x.UpdatedAt).FirstOrDefault()?.CreatedBy,
                UpdatedBy = x.OrderByDescending(x => x.UpdatedAt).FirstOrDefault()?.UpdatedBy,
                Operations = x.Select(y => new OperationByRoleResponse
                {
                    Id = y.OperationId,
                    Name = y.OperationName,
                    IsEnabled = y.IsEnabled
                }).ToList()
            })
            .Distinct()
            .ToList();

        return result;
    }

    public async Task<bool> HasPermissionAsync(int userId, string permission)
    {
        return true;
    }

    public async Task<bool> HasPermissionAsync(int userId, int permissionId)
    {
        return true;
    }

}
