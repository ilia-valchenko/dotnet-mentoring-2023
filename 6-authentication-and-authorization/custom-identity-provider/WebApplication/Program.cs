using IdentityServer4.Models;
using IdentityServer4.Test;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Services
    // AddIdentityServer() adds Authentication and Authorization.
    .AddIdentityServer()
    // Before our clients start to be exist our IdentityServer needs to be aware of them.
    // They are clients which consume protected APIs.
    .AddInMemoryClients(IdentityServer.Clients.GetClients())
    .AddInMemoryIdentityResources(IdentityServer.IdentityResources.GetIdentityResources())
    // It's our APIs. They are our APIs we are securing. It is used for identifies our APIs.
    .AddInMemoryApiResources(IdentityServer.ApiResources.GetApiResources())
    .AddInMemoryApiScopes(IdentityServer.ApiScopes.GetApiScopes())
    // AddTestUsers extension method adds support for the resource owner password grant.
    .AddTestUsers(IdentityServer.Users.GetUsers())
    // It generate a certificate for signing tokens.
    // It will create tempkey.jwk file in the solution.
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
