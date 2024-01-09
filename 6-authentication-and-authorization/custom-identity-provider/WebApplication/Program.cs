//var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

//// FYI: An API can also be a client. One API can talk to another API.

//builder.Services
//    // AddIdentityServer() adds Authentication and Authorization.
//    .AddIdentityServer()
//    // Before our clients start to be exist our IdentityServer needs to be aware of them.
//    // They are clients which consume protected APIs.
//    .AddInMemoryClients(IdentityServer.Clients.GetClients())
//    .AddInMemoryIdentityResources(IdentityServer.IdentityResources.GetIdentityResources())
//    // It's our APIs. They are our APIs we are securing. It is used for identifies our APIs.
//    .AddInMemoryApiResources(IdentityServer.ApiResources.GetApiResources())
//    .AddInMemoryApiScopes(IdentityServer.ApiScopes.GetApiScopes())
//    // AddTestUsers extension method adds support for the resource owner password grant.
//    //.AddTestUsers(IdentityServer.Users.GetUsers())
//    // It generate a certificate for signing tokens.
//    // It will create tempkey.jwk file in the solution.
//    .AddDeveloperSigningCredential();

//var app = builder.Build();

//app.UseRouting();

//// With this code, we have registered IdentityServer in our DI container using AddIdentityServer,
//// used a demo signing certificate with AddDeveloperSigningCredential, and used in-memory,
//// volatile stores for your clients, resources, and users.

//// UseIdentityServer allows IdentityServer to start handling routing for OAuth and OpenID Connect endpoints,
//// such as the authorization and token endpoints.
//app.UseIdentityServer();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapDefaultControllerRoute();
//});

//app.Run();


// ********************************************************************************** //


using IdentityServer;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using var scope = host.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        CreateManagerUser(userManager);
        CreateBuyerUser(userManager);

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    private static void CreateManagerUser(UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser("manager");

        userManager.CreateAsync(user, "password")
            .GetAwaiter()
            .GetResult();

        // FYI: You won't see the claims below in id_token or access_token
        // the MVC client app receives. If you want to see the claims
        // in the id_token you need to modify IdentityResources.
        // FYI: The claim below will be added to the identity token.
        userManager.AddClaimAsync(user, new Claim("mytest.myvalue", "big.cookie"))
            .GetAwaiter()
            .GetResult();

        // NOTE: If we want our claims to be in the access_token
        // we have to provide it in the constructor of the ApiResource.
        // (see: ApiResources.cs)

        userManager.AddClaimAsync(user, new Claim("mytest.api.myvalue", "big.api.cookie"))
            .GetAwaiter()
            .GetResult();

        userManager.AddClaimAsync(user, new Claim("role", "manager"))
            .GetAwaiter()
            .GetResult();

        // Issue: https://github.com/ThreeMammals/Ocelot/issues/679
        userManager.AddClaimAsync(user, new Claim("customrole", "manager"))
            .GetAwaiter()
            .GetResult();
    }

    private static void CreateBuyerUser(UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser("buyer");

        userManager.CreateAsync(user, "password")
            .GetAwaiter()
            .GetResult();

        // FYI: You won't see the claims below in id_token or access_token
        // the MVC client app receives. If you want to see the claims
        // in the id_token you need to modify IdentityResources.
        // FYI: The claim below will be added to the identity token.
        userManager.AddClaimAsync(user, new Claim("mytest.myvalue", "big.cookie"))
            .GetAwaiter()
            .GetResult();

        // NOTE: If we want our claims to be in the access_token
        // we have to provide it in the constructor of the ApiResource.
        // (see: ApiResources.cs)

        userManager.AddClaimAsync(user, new Claim("mytest.api.myvalue", "big.api.cookie"))
            .GetAwaiter()
            .GetResult();

        userManager.AddClaimAsync(user, new Claim("role", "buyer"))
            .GetAwaiter()
            .GetResult();
    }
}
