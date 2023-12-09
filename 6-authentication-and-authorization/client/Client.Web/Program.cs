using static System.Formats.Asn1.AsnWriter;
using static System.Net.WebRequestMethods;

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
    config.AuthorizationEndpoint = "http://localhost:5244/oauth/authorize";
    config.TokenEndpoint = "http://localhost:5244/oauth/token";
});

//http://localhost:5244/oauth/authorize?client_id=my_client_id
//&scope=
//&response_type=code
//&redirect_uri=https%3A%2F%2Flocalhost%3A7079%2Foauth%2Fcallback
//&state=CfDJ8Gbrtnk6qZFAsrdxVOBx3dVjePrQuyjkkZBlALlJK1lT7Edks7vWUmeKrJsDNBjpFFxHUpLEyUQtZxVs76MfnEdNsjJzjW5F1lDx285AJF_c9y7WuqZOZSXVPC9c1fBwGvkFRIvSXwWxo665JJdPFnR2F9sjJ0h6Q9S9RJMllR2TNQpVvwTtwgtNCVrDDiQ7KVVk3YkbsgJa8igBTcRpc8a2j3DHNQJdNaDAijZXQ184

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