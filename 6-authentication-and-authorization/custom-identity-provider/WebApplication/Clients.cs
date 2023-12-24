using IdentityServer4.Models;
using static IdentityServer4.Models.IdentityResources;
using static System.Formats.Asn1.AsnWriter;

namespace IdentityServer;

internal static class Clients
{
    public static IEnumerable<Client> GetClients()
    {
        //return new List<Client>
        //{
        //    // Here we are adding a client that uses OAuth’s client credentials grant type.
        //    // This grant type requires a client ID and client secret to authorize access.
        //    new Client
        //    {
        //        ClientId = "oauthClient",
        //        ClientName = "Client application using client credentials",
        //        AllowedGrantTypes = GrantTypes.ClientCredentials,
        //        // TODO: Do not store credentials in a code. Put it in a config.
        //        ClientSecrets = new List<Secret> {new Secret("TestSecretPassword".Sha256())},
        //        // The AllowedScopes is a list of permissions that this client is allowed to request from IdentityServer.
        //        AllowedScopes = new List<string> {"catalog-api.read"}
        //    }
        //};

        return new List<Client>
        {
            // FYI: When we use client credentials flow we usually do not want
            // to allow our client to be public accessible.
            new Client
            {
                ClientId = "my_client_id",
                ClientSecrets = new List<Secret> {new Secret("TestClientSecretValue".Sha256())},
                // 'ClientCredentials' type is used for machine to machine communication.
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "catalog-api" }
            },
            new Client
            {
                ClientId = "my_client_id_mvc",
                ClientSecrets = new List<Secret> {new Secret("TestClientMvcSecretValue".Sha256())},
                // 'Code' stands for Authorization Code Flow.
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7240/signin-oidc" },
                RequireConsent = false,
                AllowedScopes =
                {
                    "catalog-api",
                    "client-api",
                    IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                    "mytest.scope",
                    "roles"
                },
                //// The code below will force our IdentityServer
                //// to put some claims in the id_token.
                //AlwaysIncludeUserClaimsInIdToken = true

                // How does 'AlwaysIncludeUserClaimsInIdToken = true' work?
                // After receiving authorization code and exchanging it for
                // id_token and access_token. There is another call to
                // the user endpoint which requets user information.
                // And this is how it receives additional claims.
                // As far as I understood, it will add user claims to
                // the id_token not access_token.

                // Here I'm trying to configure the IdentityServer to make it
                // using refresh tokens.
                // We need to specify offline_access scope if order to get a refresh_token according to OpenID spec.
                AllowOfflineAccess = true
            },
            new Client
            {
                ClientId = "client_id_js",
                AllowedGrantTypes = GrantTypes.Implicit,
                RedirectUris = { "https://localhost:7268/home/signin" },
                RequireConsent = false,
                AllowedCorsOrigins = { "https://localhost:7268" },
                RequireClientSecret = false,
                AllowedScopes =
                {
                    "catalog-api",
                    IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                    "roles"
                },
                // We need to tell explicitly that we are allowed
                // to pass token in browser.
                AllowAccessTokensViaBrowser = true,

                AccessTokenLifetime = 1

                // FYI: We can't allow offline access with the Implicit Flow.
            },
            new Client
            {
                ClientId = "api_gateway_client_id",
                ClientSecrets = new List<Secret> {new Secret("TestClientSecretValue".Sha256())},
                //RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = {
                    "api-gateway-api",
                    "roles"
                },
                AccessTokenLifetime = 3600
            },
        };
    }
}
