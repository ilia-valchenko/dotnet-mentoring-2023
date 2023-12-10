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
    config.AuthorizationEndpoint = "https://localhost:44367/oauth/authorize";
    config.TokenEndpoint = "https://localhost:44367/oauth/token"; // We will need to send 'grant_type and=authorization_code' and 'code' and 'redirect_uri' and 'client_id'.
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