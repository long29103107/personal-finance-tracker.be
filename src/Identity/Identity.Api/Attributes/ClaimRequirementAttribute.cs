using Microsoft.AspNetCore.Authorization;

namespace Identity.Api.Attributes;

public class ClaimRequirementAttribute : IAuthorizationRequirement
{
    public string Scope { get; set; }
    public string Operation { get; set; }
    public int UserId { get; set; }

    public ClaimRequirementAttribute(int userId, string scope, string operation)
    {
        Scope = scope;
        Operation = operation;
        UserId = userId;
    }
}
