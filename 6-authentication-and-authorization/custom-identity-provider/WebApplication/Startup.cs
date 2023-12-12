using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer;

public class Startup
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration config, IWebHostEnvironment env)
    {
        _config = config;
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(config =>
        {
            config.UseInMemoryDatabase("Memory");
        });

        // AddIdentity method registers the services.
        services.AddIdentity<IdentityUser, IdentityRole>(config =>
        {
            config.Password.RequiredLength = 4;
            config.Password.RequireDigit = false;
            config.Password.RequireNonAlphanumeric = false;
            config.Password.RequireUppercase = false;
            //config.SignIn.RequireConfirmedEmail = true;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(config =>
        {
            config.Cookie.Name = "IdentityServer.Cookie";
            config.LoginPath = "/Auth/Login";
        });

        // FYI: An API can also be a client. One API can talk to another API.

        services
            // AddIdentityServer() adds Authentication and Authorization.
            .AddIdentityServer()
            // Now IdentityServer will be aware of the user
            // and how to query for this model.
            .AddAspNetIdentity<IdentityUser>()
            // Before our clients start to be exist our IdentityServer needs to be aware of them.
            // They are clients which consume protected APIs.
            .AddInMemoryClients(IdentityServer.Clients.GetClients())
            .AddInMemoryIdentityResources(IdentityServer.IdentityResources.GetIdentityResources())
            // It's our APIs. They are our APIs we are securing. It is used for identifies our APIs.
            .AddInMemoryApiResources(IdentityServer.ApiResources.GetApiResources())
            .AddInMemoryApiScopes(IdentityServer.ApiScopes.GetApiScopes())
            // AddTestUsers extension method adds support for the resource owner password grant.
            //.AddTestUsers(IdentityServer.Users.GetUsers())
            // It generate a certificate for signing tokens.
            // It will create tempkey.jwk file in the solution.
            .AddDeveloperSigningCredential();

        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        // With this code, we have registered IdentityServer in our DI container using AddIdentityServer,
        // used a demo signing certificate with AddDeveloperSigningCredential, and used in-memory,
        // volatile stores for your clients, resources, and users.

        // UseIdentityServer allows IdentityServer to start handling routing for OAuth and OpenID Connect endpoints,
        // such as the authorization and token endpoints.
        app.UseIdentityServer();

        //if (_env.IsDevelopment())
        //{
        //    app.UseCookiePolicy(new CookiePolicyOptions()
        //    {
        //        MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax
        //    });
        //}

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    }
}