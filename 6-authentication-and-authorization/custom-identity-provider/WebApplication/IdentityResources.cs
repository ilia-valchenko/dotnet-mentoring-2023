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

        // THE ARRAY BELOW IS A POSSIBLE SCOPE THAT CAN BE REQUESTED.
        // The array below helps us to make the IdentityServer being
        // aware of scoeps.
        // It doesn't know yet what client is allowed to request this scope.
        // In order to allow the client to request these scopes we need
        // to modify Clients.cs file.
        // IMPORTANT! This is an information about the user
        // which will go to the id_token. If we want to add a new
        // claim and want to see this claim in the id_token
        // we need to specify it here.
        return new IdentityServer4.Models.IdentityResource[]
        {
            new IdentityServer4.Models.IdentityResources.OpenId(),
            // The Profile scope we specified here comes with many claims.
            new IdentityServer4.Models.IdentityResources.Profile(),
            new IdentityServer4.Models.IdentityResource
            {
                // This is a scope.
                Name = "mytest.scope",
                // Now we need to specify the claims which
                // come with this scope.
                UserClaims =
                {
                    // I defined this claim in the Program.cs
                    "mytest.myvalue"
                }
            }
        };
    }
}