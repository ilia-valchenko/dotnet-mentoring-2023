using Microsoft.AspNetCore.Authorization;

namespace Client.Web.AuthorizationRequirements.Extensions;

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