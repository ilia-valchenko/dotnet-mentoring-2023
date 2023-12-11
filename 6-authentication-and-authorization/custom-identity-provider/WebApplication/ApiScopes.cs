using IdentityServer4.Models;

namespace IdentityServer;

internal static class ApiScopes
{
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        //return new[]
        //{
        //    new ApiScope("catalog-api.read", "Read Access to Catalog API"),
        //    new ApiScope("catalog-api.create", "Create Access to Catalog API"),
        //    new ApiScope("catalog-api.update", "Update Access to Catalog API"),
        //    new ApiScope("catalog-api.delete", "Delete Access to Catalog API")
        //    new ApiScope("catalog-api", "Full access to Catalog API")
        //};

        return new[]
        {
            new ApiScope("catalog-api", "Full access to Catalog API"),
            new ApiScope("client-api")
        };
    }
}
