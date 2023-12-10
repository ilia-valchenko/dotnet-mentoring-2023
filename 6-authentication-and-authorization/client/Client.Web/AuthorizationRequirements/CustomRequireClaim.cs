using Microsoft.AspNetCore.Authorization;

namespace Client.Web.AuthorizationRequirements;

public class CustomRequireClaim : IAuthorizationRequirement
{
    public CustomRequireClaim(string claimType)
    {
        ClaimType = claimType;
    }

    public string ClaimType { get; }
}