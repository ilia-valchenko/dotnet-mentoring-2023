using Microsoft.AspNetCore.Authorization;

namespace RestfulWebApi.Api.AuthorizationRequirements.Extensions;

public static class AuthorizationPolicyBuilderExtensions
{
    public static AuthorizationPolicyBuilder AddRequireCustomClaimRequirement(
            this AuthorizationPolicyBuilder builder,
            string claimType)
    {
        builder.AddRequirements(new CustomRequireClaimRequirement(claimType));
        return builder;
    }
}