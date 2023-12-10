using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DummyIdentityServer.Web.Controllers;

public class OAuthController : Controller
{
    private const int CodeSize = 10;

    [HttpGet]
    public IActionResult Authorize(
        string response_type, // Authorization flow type. Example: response_type="code"
        string client_id, // Example: client_id="my_client_id"
        string redirect_uri, // Example: redirect_uri="https://localhost:7079/oauth/callback"
        string scope, // What information I request. Example: email, tel.
        string state) // Random string generated to confirm that we are going to back to the same client.
                      // The client generate this state string. If the state changes once we go back to our
                      // client this is essentially going to error that basically we haven't requested this authorization.
    {
        var query = new QueryBuilder();
        query.Add("redirectUri", redirect_uri);
        query.Add("state", state);

        // ?redirectUri=https%3A%2F%2Flocalhost%3A7079%2Foauth%2Fcallback&state=CfDJ8Gbrtnk6qZFAsrdxVOBx3dVvKl74txaqTeF2XgqhGRRXJcUkEXNxcKyxvs16N
        return View(model: query.ToString());
    }

    [HttpPost]
    public IActionResult Authorize(
        string username,
        string redirectUri, // Example: redirectUri="https://localhost:7079/oauth/callback"
        string state) // The state has to persist once we go back in order to confirm
                      // that is coming back from the correct authorization endpoint.
    {
        // FYI: It has about 5-10 minutes expiry time.
        var code = GenerateRandomString(CodeSize);

        var query = new QueryBuilder();
        query.Add("code", code);
        query.Add("state", state);

        // redirectUri="https://localhost:7079/oauth/callback"
        // query="?code=BLXBKPOQTL&state=CfDJ8Gbrtnk6qZFAsrdxVOBx3dVvKl74txaqTeF2XgqhGRRXJcUkEXNxcKyxvs16N"
        // https://localhost:7079/oauth/callback?code=BLXBKPOQTL&state=CfDJ8Gbrtnk6qZFAsrdxVOBx3dVvKl74txaqTeF2XgqhGRRXJcUkEXNxcKyxvs16N
        return Redirect($"{redirectUri}{query.ToString()}");
    }

    public async Task<IActionResult> Token(
        string grant_type, // Flow of access_token request. Example: grant_type="authorization_code"
        string code, // Confirmation of the authentication process. Example: code="RKWFYAZZLE"
        string redirect_uri, // Example: redirectUri="https://localhost:7079/oauth/callback"
        string client_id) // Example: client_id="my_client_id"
    {
        // Some mechanism for validating the code.
        // The code is usually stored in a database.
        // We also need to check whether it has been expired or not.

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "testid"),
            new Claim("mycustomclaim", "customclaimvalue")
        };

        var secretBytes = Encoding.UTF8.GetBytes(Constants.Constants.SecretKey);
        var key = new SymmetricSecurityKey(secretBytes);
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            Constants.Constants.Issuer,
            Constants.Constants.Audience,
            claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(1),
            signingCredentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        var response = new
        {
            access_token = accessToken,
            token_type = "Bearer",
            raw_claim = "oauthTutorial"
        };

        var responseJson = JsonConvert.SerializeObject(response);
        var responseBytes = Encoding.UTF8.GetBytes(responseJson);
        await Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);

        //// *** ResponseJson value ***
        //{
        //    "access_token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzb21lX2lkIiwibXlfY3VzdG9tX2NsYWltIjoiY3VzdG9tX2NsYWltX3ZhbHVlIiwibmJmIjoxNzAyMTk5MjUzLCJleHAiOjE3MDIyMDI4NTMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzY3LyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8ifQ.EZuSBfuKrpG6NKsHFcd6ZQzRlCyCrGhzHDlmKUu-D3U",
        //    "token_type":"Bearer",
        //    "raw_claim":"oauthTutorial"
        //}

        //// *** Decoded access_token ***
        //{
        //    "sub": "testid",
        //    "mycustomclaim": "customclaimvalue",
        //    "nbf": 1702199253,
        //    "exp": 1702202853,
        //    "iss": "https://localhost:44367/",
        //    "aud": "http://localhost:5000/"
        //}

        // After that we will see a new cookie in our browser.
        // Cookie name: .AspNetCore.ClientCookie

        // redirect_uri="https://localhost:7079/oauth/callback"
        return Redirect(redirect_uri);
    }

    // An example of successful response:
    // HTTP/1.1 200 OK
    // Content-Type: application/json;charset=UTF-8
    // Cache-Control: no-store
    // Pragma: no-cache

    //{
    // "access_token": "2YotnFZFEjr1zCsicCMWpAA",
    // "token_type": "example",
    // "expires_in": 3600,
    // "refresh_token": "tGzv3JOkF0XGQx2TlKWIA",
    // "example_parameter": "example_value"
    //}

    private static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var random = new Random();

        char[] randomArray = new char[length];

        for (int i = 0; i < length; i++)
        {
            randomArray[i] = chars[random.Next(chars.Length)];
        }

        return new string(randomArray);
    }
}