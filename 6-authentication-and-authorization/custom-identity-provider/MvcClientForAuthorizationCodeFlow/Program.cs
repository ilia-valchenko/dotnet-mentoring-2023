using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Now we are using OpenID Connect flow.
// We will have to add OpenID Connect extension.
builder.Services.AddAuthentication(config =>
{
    config.DefaultScheme = "Cookie"; // "CustomCookieAuthenticationSchema"
    config.DefaultChallengeScheme = "oidc"; // "CustomOidcScheme"
})
.AddCookie("Cookie" /*"CustomCookieAuthenticationSchema"*/)
// Instead of having an access token only we will also have an id token.
// BTW. This is OIDC. This middleware knows how to retrieve the discovery document.
.AddOpenIdConnect("oidc" /*"CustomOidcScheme"*/, config =>
{
    config.ClientId = "my_client_id_mvc";
    config.ClientSecret = "TestClientMvcSecretValue";
    config.Authority = "https://localhost:7193/";
    // We tell the app to save tokens and cookies.
    config.SaveTokens = true;

    // The flows that we can use here:
    // - Authorization Code Flow (response_type=code)
    // - Implicit Flow (response_type=id_token OR response_type=id_token token)
    // - Hybrid Flow (response_type=code token OR response_type=code id_token token)

    // We will use Authorization Code Flow.
    // Note: response_type != grant_type.
    // The grant_type is a flow of access token request.
    // The response_type is authorization flow type or maybe rather actually authentication.

    // response_type=code means that we're gonna get an authorization code back
    // which we can then exchange for ID token and token.

    config.ResponseType = "code";

    // FYI: OpenID automatically populates `scope`
    // with `openid` and 'profile' scope values.
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
