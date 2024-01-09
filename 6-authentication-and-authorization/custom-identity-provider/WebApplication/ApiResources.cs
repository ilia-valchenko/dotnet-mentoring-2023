using IdentityServer4.Models;

namespace IdentityServer;

internal static class ApiResources
{
    public static IEnumerable<ApiResource> GetApiResources()
    {
        //return new[]
        //{
        //    new ApiResource
        //    {
        //        Name = "catalog-api",
        //        DisplayName = "Catalog API",
        //        Description = "Allow the application to access Catalog API on your behalf",
        //        ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
        //        UserClaims = new List<string> {"role"},
        //        Scopes = new List<string> {
        //            "catalog-api.read",
        //            "catalog-api.create",
        //            "catalog-api.update",
        //            "catalog-api.delete"
        //        }
        //    }
        //};

        // Here we register our APIs.
        return new[]
        {
            new ApiResource
            {
                Name = "catalog-api",
                Scopes = { "catalog-api" },
                UserClaims =
                {
                    "role",
                    //"mytest.api.myvalue"
                }
            },
            //// NOTE: If we want our claims to be in the access_token
            //// we have to provide it in the constructor of the ApiResource.
            //new ApiResource(
            //    "catalog-api",
            //    new string[]
            //    {
            //        "catalog-api",
            //        "role",
            //        "mytest.api.myvalue"
            //    }),
            new ApiResource
            {
                Name = "client-api",
                Scopes = { "client-api" }
            },
            new ApiResource
            {
                Name = "API Gateway API resource", // This value will be used as "aud" field value in the JWT.
                Scopes = { "api-gateway-api" },
                UserClaims =
                {
                    "role",
                    // Issue: https://github.com/ThreeMammals/Ocelot/issues/679
                    "customrole"
                }
            }
        };
    }
}