using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Catalog.Api.AuthorizationRequirements;

public class JwtRequirementHandler : AuthorizationHandler<JwtRequirement>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpContext _httpContext; // We will extract the token from the Header.

    // We need a constructor, because we are gonna talk to the IdentityServer
    // in order to forward our token.
    public JwtRequirementHandler(
        IHttpClientFactory httpClientFactory,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContext = httpContextAccessor.HttpContext;
    }

    // Note: Don't forget to register this handler in the Startup.
    // defaultAuthBuilder.AddRequirements(new JwtRequirement())
    // services.AddScoped<IAuthorizationHandler, JwtRequirementHandler>();
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        JwtRequirement requirement)
    {
        if (_httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            var accessToken = authHeader.ToString().Split(' ')[1];
            using var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(
                $"https://localhost:44367/oauth/validatetoken?access_token={accessToken}");

            if (response.IsSuccessStatusCode)
            {
                context.Succeed(requirement);
            }
        }
    }
}