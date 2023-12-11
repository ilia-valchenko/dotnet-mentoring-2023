﻿using IdentityServer4.Models;

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
                Scopes = { "catalog-api" }
            },
            new ApiResource
            {
                Name = "client-api",
                Scopes = { "client-api" }
            }
        };
    }
}