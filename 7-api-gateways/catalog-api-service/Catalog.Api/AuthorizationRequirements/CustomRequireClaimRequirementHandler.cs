using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Api.AuthorizationRequirements;

public class CustomRequireClaimRequirementHandler : AuthorizationHandler<CustomRequireClaimRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        CustomRequireClaimRequirement requirement)
    {
        var hasClaim = context.User.Claims.Any(x => x.Type == requirement.ClaimType);

        if (hasClaim)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}