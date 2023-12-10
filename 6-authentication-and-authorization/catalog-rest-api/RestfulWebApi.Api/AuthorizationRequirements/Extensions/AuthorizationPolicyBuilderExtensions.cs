using Microsoft.AspNetCore.Authorization;

namespace RestfulWebApi.Api.AuthorizationRequirements.Extensions;

public static class AuthorizationPolicyBuilderExtensions
{
    public static AuthorizationPolicyBuilder RequireCustomClaim(
            this AuthorizationPolicyBuilder builder,
            string claimType)
    {
        builder.AddRequirements(new CustomRequireClaim(claimType));
        return builder;
    }
}