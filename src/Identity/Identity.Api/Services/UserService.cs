using Identity.Api.Entities;
using Identity.Api.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using Identity.Api.Exceptions;
using Identity.Api.DependencyInjection.Extensions.Mappings;
using Identity.Api.Repositories.Abstractions;
using Shared.Service;
using ILogger = Serilog.ILogger;
using static Shared.Dtos.Identity.UserDtos;
using FilteringAndSortingExpression.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Services;

public class UserService : BaseService<IRepositoryManager>, IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepo;

    public UserService(ILogger logger, UserManager<User> userManager, IUserRepository userRepo, IRepositoryManager repoManager) 
        : base(logger, repoManager)
    {
        _userManager = userManager;
        _userRepo = userRepo;
    }

    public async Task<User> CreateOrFindUserAsync(UserCreateOrFindRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user != null)
        {
            return user;
        }

        user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "LoginGoogle",
        };

        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
        }

        return user;
    }


    #region Get
    public async Task<UserResponse> GetAsync(int id)
    {
        var user = await _GetUserAsync(id)
           ?? throw new UserException.NotFound(id);

        return user.ToUserResponse();
    }
    #endregion

    #region Get Active User
    public async Task<UserResponse> GetActiveAsync(int id)
    {
        var user = await _GetActiveUserAsync(id)
           ?? throw new UserException.NotFound(id);

        return user.ToUserResponse();
    }
    #endregion

    #region Delete
    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _GetUserAsync(id)
            ?? throw new UserException.NotFound(id);

        _CheckLockUser(user);

        _userRepo.Remove(user);
        try
        {
            await _userRepo.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return false;
        }

        return true;
    }
    #endregion

    #region Assign role to user
    public async Task AssignedRoleAsync(int userId, int roleId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new UserException.NotFound(userId);

        if (!user.IsActive)
        {
            throw new BadRequestException($"User is deactivated");
        }

        var role = await _repoManager.Role.FindByCondition(x => x.Id == roleId)
            .FirstOrDefaultAsync()
            ?? throw new RoleException.NotFound(roleId);

        if (!role.IsActive)
        {
            throw new BadRequestException("Role is deactivated");
        }

        var existingUserRole = await _repoManager.UserRole.FindByCondition(x =>
                x.RoleId == role.Id
                && x.UserId == userId)
            .AnyAsync();

        if (!existingUserRole)
        {
            _repoManager.UserRole.Add(new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            });
            await _repoManager.SaveAsync();
        }
    }
    #endregion

    #region Has Permission 
    public async Task<bool> HasPermissionAsync(int userId, int permissionId)
    {
        var result = false;
        var user = await _GetActiveUserAsync(userId)
           ?? throw new UserException.NotFound(userId);

        var query = _repoManager.AccessRule
            .FindByCondition(x => x.Mode == true
                && x.Role.UserRoles.Any(y => y.User.Id == userId)
                && true == x.Role.IsActive
                && x.PermissionId == permissionId)
            .Include(x => x.Permission)
                .ThenInclude(x => x.Scope)
            .Include(x => x.Permission)
                .ThenInclude(x => x.Operation)
            .Include(x => x.Role)
                .ThenInclude(x => x.UserRoles)
                .ThenInclude(x => x.User);

        result = await query.AnyAsync();

        return result;
    }

    public async Task<bool> HasPermissionAsync(int userId, UserHasPermissionRequest request)
    {
        var result = false;
        var user = await _GetActiveUserAsync(userId)
           ?? throw new UserException.NotFound(userId);

        var query = _repoManager.AccessRule
            .FindByCondition(x => x.Mode == true
                && x.Role.UserRoles.Any(y => y.User.Id == userId)
                && true == x.Role.IsActive
                && x.Permission.Scope.Code == request.ScopeCode
                && x.Permission.Operation.Code == request.OperationCode)
            .Include(x => x.Permission)
                .ThenInclude(x => x.Scope)
            .Include(x => x.Permission)
                .ThenInclude(x => x.Operation)
            .Include(x => x.Role)
                .ThenInclude(x => x.UserRoles)
                .ThenInclude(x => x.User);

        result = await query.AnyAsync();

        return result;
    }
    #endregion

    #region Get List
    public async Task<IEnumerable<UserResponse>> GetListAsync(UserListRequest request)
    {
        var result = await _UserIgnoreGlobalFilter()
            .Select(x => x.ToUserResponse())
            .Filter(request)
            .ToListAsync();

        return result;
    }
    #endregion

    #region Update
    public async Task<UserResponse> UpdateAsync(int id, UserUpdateRequest request)
    {
        var user = await _GetUserAsync(id)
          ?? throw new UserException.NotFound(id);

        _CheckLockUser(user);

        var isExistingUser = await _repoManager.User.FindByCondition(x =>
                x.Email.Equals(request.Email)).AnyAsync();

        if (isExistingUser)
        {
            throw new BadRequestException("Existing role has code or name!");
        }

        user.UserName = request.UserName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.IsLocked = request.IsLocked;
        user.IsActive = request.IsActive;

        _repoManager.User.Update(user);
        await _repoManager.SaveAsync();

        return user.ToUserResponse();
    }
    #endregion

    #region Common Private

    private async Task<User> _GetActiveUserAsync(int id)
    {
        return await _repoManager.User.FirstOrDefaultAsync(x => x.Id == id);
    }

    private async Task<User> _GetUserAsync(int id)
    {
        return await _UserIgnoreGlobalFilter().FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<User> _UserIgnoreGlobalFilter()
    {
        return _repoManager.User.FindAll().IgnoreQueryFilters();
    }

    public void _CheckLockUser(User user)
    {
        if (user.IsLocked)
        {
            throw new BadRequestException("The user has created from system, cannot update this one!");
        }
    }
    #endregion
}
