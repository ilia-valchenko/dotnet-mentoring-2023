using IdentityServer4.Models;

namespace IdentityServer;

internal static class IdentityResources
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        //return new IdentityServer4.Models.IdentityResource[]
        //{
        //    new IdentityServer4.Models.IdentityResources.OpenId(),
        //    new IdentityServer4.Models.IdentityResources.Profile(),
        //    new IdentityServer4.Models.IdentityResources.Email(),
        //    new IdentityServer4.Models.IdentityResource
        //    {
        //        Name = "role",
        //        UserClaims = new List<string> {"role"}
        //    }
        //};

        return new IdentityServer4.Models.IdentityResource[]
        {
            new IdentityServer4.Models.IdentityResources.OpenId(),
            new IdentityServer4.Models.IdentityResources.Profile()
        };
    }
}