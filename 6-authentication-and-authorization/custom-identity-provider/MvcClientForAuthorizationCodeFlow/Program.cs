var builder = WebApplication.CreateBuilder(args);

// Now we are using OpenID Connect flow.
// We will have to add OpenID Connect extension.
builder.Services.AddAuthentication(config =>
{
    config.DefaultScheme = "CustomCookieAuthenticationSchema";
    config.DefaultChallengeScheme = "CustomOidcScheme";
})
.AddCookie("CustomCookieAuthenticationSchema")
// Instead of having an access token only we will also have an id token.
// BTW. This is OIDC. This middleware knows how to retrieve the discovery document.
.AddOpenIdConnect("CustomOidcScheme", config =>
{
    config.ClientId = "my_client_id_mvc";
    config.ClientSecret = "TestClientMvcSecretValue";
    // We tell the app to save tokens and cookies.
    config.SaveTokens = true;
    config.Authority = "https://localhost:7193/";
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
