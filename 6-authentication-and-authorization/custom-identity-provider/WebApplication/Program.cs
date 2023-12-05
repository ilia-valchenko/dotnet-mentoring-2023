using IdentityServer4.Models;
using IdentityServer4.Test;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryClients(new List<Client>())
    .AddInMemoryIdentityResources(new List<IdentityResource>())
    .AddInMemoryApiResources(new List<ApiResource>())
    .AddInMemoryApiScopes(new List<ApiScope>()) // Scopes are group of claims.
    .AddTestUsers(new List<TestUser>())
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseRouting();

// With this code, we have registered IdentityServer in our DI container using AddIdentityServer,
// used a demo signing certificate with AddDeveloperSigningCredential, and used in-memory,
// volatile stores for your clients, resources, and users.

// UseIdentityServer allows IdentityServer to start handling routing for OAuth and OpenID Connect endpoints,
// such as the authorization and token endpoints.
app.UseIdentityServer();

app.MapGet("/", () => "Hello World!");

app.Run();
