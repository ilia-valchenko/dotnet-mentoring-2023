using IdentityServer4.Models;

namespace WebApplication;

internal static class Resources
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };
    }

    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new[]
        {
            new ApiResource
            {
                Name = "catalog-api",
                DisplayName = "Catalog API",
                Description = "Allow the application to access Catalog API on your behalf",
                ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"},
                Scopes = new List<string> {
                    "catalog-api.read",
                    "catalog-api.create",
                    "catalog-api.update",
                    "catalog-api.delete"
                }
            }
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new[]
        {
            new ApiScope("catalog-api.read", "Read Access to Catalog API"),
            new ApiScope("catalog-api.create", "Create Access to Catalog API"),
            new ApiScope("catalog-api.update", "Update Access to Catalog API"),
            new ApiScope("catalog-api.delete", "Delete Access to Catalog API")
        };
    }
}