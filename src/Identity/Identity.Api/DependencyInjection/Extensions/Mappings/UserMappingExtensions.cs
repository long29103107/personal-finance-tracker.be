using Identity.Api.Entities;
using static Shared.Dtos.Identity.UserDtos;

namespace Identity.Api.DependencyInjection.Extensions.Mappings;

public static class GoalMappingExtension
{
    public static UserResponse ToUserResponse(this User user)
    {
        return new UserResponse()
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}
