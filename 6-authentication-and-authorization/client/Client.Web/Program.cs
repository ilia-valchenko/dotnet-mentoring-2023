var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(config =>
{
    // We check the cookie to confirm that we are authenticated.
    config.DefaultAuthenticateScheme = "ClientCookie";
    // When we sign in we will deal out a cookie.
    config.DefaultSignInScheme = "ClientCookie";
    // Use this to check if we are allowed to do something.
    // It will use our OAuth flow to find out if we are allowed to do something.
    config.DefaultChallengeScheme = "OurServer";
})
.AddCookie("ClientCookie")
.AddOAuth("OurServer", config =>
{
    config.ClientId = "my_client_id";
    config.ClientSecret = "my_client_secret";
    config.CallbackPath = "/oauth/callback";
    config.AuthorizationEndpoint = "http://localhost:5000/api/v1/oauth/authorize";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();