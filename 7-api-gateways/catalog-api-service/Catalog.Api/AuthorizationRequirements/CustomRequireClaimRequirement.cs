using Microsoft.AspNetCore.Authorization;

namespace Catalog.Api.AuthorizationRequirements;

public class CustomRequireClaimRequirement : IAuthorizationRequirement
{
    public CustomRequireClaimRequirement(string claimType)
    {
        ClaimType = claimType;
    }

    public string ClaimType { get; }
}