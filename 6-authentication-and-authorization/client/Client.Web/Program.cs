using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(config =>
{
    // We will see a new cookie in our browser.
    // Cookie name: .AspNetCore.ClientCookie

    // We check the cookie to confirm that we are authenticated.
    // This is how we are authenticated. How will we use an access token
    // after authentication is up to us.
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
    config.AuthorizationEndpoint = "https://localhost:44367/oauth/authorize";
    config.TokenEndpoint = "https://localhost:44367/oauth/token"; // We will need to send 'grant_type and=authorization_code' and 'code' and 'redirect_uri' and 'client_id'.
    config.SaveTokens = true; // Defines whether access and refresh tokens should be stored in the Microsoft.AspNetCore.Authentication.AuthenticationProperties after a successful authorization.
    config.Events = new OAuthEvents
    {
        // This is a function which is going to return a Task.
        OnCreatingTicket = context =>
        {
            var accessToken = context.AccessToken;
            var base64Payload = accessToken.Split('.')[1];
            var jsonPayload = Base64UrlEncoder.Decode(base64Payload);
            var claims = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonPayload);

            // *** Example ***
            // "sub": "testid",
            // "mycustomclaim": "customclaimvalue",
            // "nbf": 1702208978,
            // "exp": 1702212578,
            // "iss": "https://localhost:44367/",
            // "aud": "https://localhost:5000/"

            foreach (var claim in claims)
            {
                context.Identity.AddClaim(new Claim(claim.Key, claim.Value));
            }

            return Task.CompletedTask;
        }
    };
});

//// *** AuthorizationEndpoint ***
//https://localhost:44367/oauth/authorize?client_id=my_client_id
//&scope=
//&response_type=code
//&redirect_uri=https%3A%2F%2Flocalhost%3A7079%2Foauth%2Fcallback
//&state=CfDJ8Gbrtnk6qZFAsrdxVOBx3dVjePrQuyjkkZBlALlJK1lT7Edks7vWUmeKrJsDNBjpFFxHUpLEyUQtZxVs76MfnEdNsjJzjW5F1lDx285AJF

//// *** AuthorizationEndpoint (redirect response) ***
//https://localhost:7079/oauth/callbackcode=JQBRMYOYVB
//&state=CfDJ8Gbrtnk6qZFAsrdxVOBx3dUOUzGV1i5u59wvu_h1aEe4oFJ7nxKCRJcdpHIH_4G5oGQyvg_87fKHKRJHRe4zEP8uE553QBprntaGpwc5

//// *** TokenEndpoint (incoming parameters) ***
//grant_type = "authorization_code"
//code = "RKWFYAZZLE"
//redirectUri = "https://localhost:7079/oauth/callback"
//client_id = "my_client_id"

// After that we will see a new cookie in our browser.
// Cookie name: .AspNetCore.ClientCookie

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

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