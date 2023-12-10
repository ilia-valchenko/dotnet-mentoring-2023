using Microsoft.AspNetCore.Authorization;

namespace RestfulWebApi.Api.AuthorizationRequirements;

public class CustomRequireClaim : IAuthorizationRequirement
{
    public CustomRequireClaim(string claimType)
    {
        ClaimType = claimType;
    }

    public string ClaimType { get; }
}