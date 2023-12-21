using Microsoft.AspNetCore.Hosting.Server;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Implicit Flow is good for application where the client lives on the user-agent (browser or mobile device).
// Example: Native application or SPA.
// FYI: Our MVC client is really living on the server cause all the code lives on the server.
// In case of native application or SPA the code is on the user agent. It's on the device.
// That means things are compromised.
// NOTE: For Implicit Flow we are not allowed for a refresh token. There are other ways
// keeping the access token up-to-date.

// We need to use 'response_type=id_token' OR 'response_type=id_token token' for the Implicit Flow.
// 'id_token' is authentication token. 'token' is an eccess token.
// We will use both 'id_token' and 'token'.
// see: https://openid.net/specs/openid-connect-core-1_0.html#toc

//Implicit Flow Auth required parameters:
//-responce_type
//- redirect_uri
//- nonce

//// *** Example ***
//GET/authorize?
//  response_type=id_token%20token
//  &client_id=s6BhdRkqt3
//  &redirect_uri=https%3A%2F%2Fclient.example.org%2Fcb
//  &scope=openid%20profile
//  &state=af0ifjsldkj
//  &nonce=n-0S6_WzA2Mj HTTP/1.1
//  Host: server.example.com

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
