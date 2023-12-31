using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Now we are using OpenID Connect flow.
// We will have to add OpenID Connect extension.
builder.Services.AddAuthentication(config =>
{
    // The cookies I see in my browser:
    // - .AspNetCore.Cookie // This is authentication session on the MVC side. It's called "Cookie", but might be called "CustomCookieAuthenticationSchema" (see the code below).
    // - .AspNetCore.CookieC1 // C1 and C2 stand for chunk. The cookie it too big. That's why it was splitted.
    // - .AspNetCore.CookieC2
    // - IdentityServer.Cookie // This is authentication session on my IdentityServer side. This names comes from the custom IdentityServer (see: config.Cookie.Name = "Identity.Cookie").

    // .AspNetCore.Cookie holds some state which references to this session
    // holding our id_token and access_token.
    // I can think about this cookie as about authentication for this client app (ASP.NET MVC app).
    // The cookie holding the id_token which is used for authenticate the client
    // and the access_token which is going to be used to authenticate with the API
    // or another secured resource.

    config.DefaultScheme = "Cookie"; // "CustomCookieAuthenticationSchema"
    config.DefaultChallengeScheme = "oidc"; // "CustomOidcScheme"
})
.AddCookie("Cookie" /*"CustomCookieAuthenticationSchema"*/)
// Instead of having an access token only we will also have an id token.
// BTW. This is OIDC. This middleware knows how to retrieve the discovery document.
.AddOpenIdConnect("oidc" /*"CustomOidcScheme"*/, config =>
{
    config.SignInScheme = "Cookie";
    config.ClientId = "my_client_id_mvc";
    config.ClientSecret = "TestClientMvcSecretValue";
    config.Authority = "https://localhost:7193/";
    // We tell the app to save tokens and cookies.
    config.SaveTokens = true;

    // Configure cookie claim mapping.
    config.ClaimActions.MapUniqueJsonKey("mytest.myvalue", "mytest.myvalue");
    config.ClaimActions.MapUniqueJsonKey("role", "role");
    // JFYI: We can also delete a claim from my cookie session.
    config.ClaimActions.DeleteClaim("amr");
    config.ClaimActions.DeleteClaim("s_hash");

    config.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "role"
    };

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
    // with `openid` and 'profile' scope values
    // because they are mandatory according to OpenID specification.

    // The code below will force our OpenIdconnect middleware
    // doing another round trip after receiving the id_token
    // in order to get additional claims.
    // Two trips to load claims into the cookie,
    // but the id_token is smaller.
    config.GetClaimsFromUserInfoEndpoint = true;

    // By using the code below we forcing our client to
    // request specific scopes defined in IdentityServer.
    //config.Scope.Clear();
    //config.Scope.Add("openid"); // Add mandatory 'openid' scope in case you clearing scopes.
    //config.Scope.Add("mytest.scope");
    config.Scope.Add("roles");
    config.Scope.Add("catalog-api");
    // We need to specify offline_access scope if order to get a refresh_token according to OpenID spec.
    config.Scope.Add("offline_access");
});

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

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
