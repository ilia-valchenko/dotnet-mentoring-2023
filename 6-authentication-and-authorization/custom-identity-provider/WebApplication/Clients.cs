using IdentityServer4.Models;

namespace WebApplication;

internal static class Clients
{
    public static IEnumerable<Client> Get()
    {
        return new List<Client>
        {
            // Here we are adding a client that uses OAuth’s client credentials grant type.
            // This grant type requires a client ID and client secret to authorize access.
            new Client
            {
                ClientId = "oauthClient",
                ClientName = "Example client application using client credentials",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // TODO: Do not store credentials in a code. Put it in a config.
                ClientSecrets = new List<Secret> {new Secret("TestSecretPassword".Sha256())},
                // The AllowedScopes is a list of permissions that this client is allowed to request from IdentityServer.
                AllowedScopes = new List<string> {"api1.read"}
            }
        };
    }
}