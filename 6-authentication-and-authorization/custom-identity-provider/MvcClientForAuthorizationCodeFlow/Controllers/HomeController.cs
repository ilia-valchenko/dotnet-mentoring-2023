using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcClientForAuthorizationCodeFlow.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Index()
    {
        return View();
    }

    // When we will try to open the Secret page below,
    // we will be redirected.

    //https://localhost:7193/connect/authorize?client_id=my_client_id_mvc
    //    &redirect_uri=https%3A%2F%2Flocalhost%3A7240%2Fsignin-oidc
    //    &response_type=code
    //    &scope=openid%20profile
    //    &code_challenge=S83nm5MFgUDnh3yAmODcvBUgkqEXdhGtfqvDB-oDm4s
    //    &code_challenge_method=S256
    //    &response_mode=form_post
    //    &nonce=638379163989015268.Yjc3MDlmNzQtMWE3MC00ZGU0LWJkZjQtMTQyOGEyYjQwNjU4NGY0MTJiYTAtMTQ2Ni00MWEwLWFjYjMtODRlNTBjYTFmZjFl
    //    &state=CfDJ8Gbrtnk6qZFAsrdxVOBx3dVy0wdJg5OOM9lO8n0JTrhsdMgZeLGEyNyI35-nRRurg9HaBVPNiM7y7oWM1joqpmwDevMhgCZidExDY

    //[Authorize]
    [Authorize(Roles = "manager, buyer")]
    public async Task<IActionResult> Secret()
    {
        // Let's try to extract
        // - id_token (It's just an authentication information rather than user information)
        // - access_token
        // - refresh_token (optional by specification)

        //// *** 13 claims in total ***
        //{
        //    "nbf": 1702360005, // When the token starts to being valid.
        //    "exp": 1702360305, // When the token expires.
        //    "iss": "https://localhost:7193", // Who issued the token.
        //    "aud": "my_client_id_mvc",
        //    "nonce": "638379567903822169.OWQ0MTUxMzAtM2VhMi00NTRhLWE5MjktNmY4MmRhYmFkNTI1ZDI0YTBjYWMtNjczZC00OGVhLTgxNTYtNmJlMWE4YmMzZGY4",
        //    "iat": 1702360005,
        //    "at_hash": "_ELfkCq75bnuMFxQa7C8WA",
        //    "s_hash": "deXynX0wgtwC3yzg-2J5yQ",
        //    "sid": "625D897A05BDE6AF274E94ED603C405F",
        //    "sub": "835c88ee-977e-42cf-9027-10182c27fbb2", // Basically the id of our user.
        //    "auth_time": 1702360005,
        //    "idp": "local",
        //    "amr": [ // Authentication methods.
        //        "pwd"
        //    ]
        //}

        //// The new id_token value after settingAlwaysIncludeUserClaimsInIdToken = true
        //// in the IdentityServer.
        //{
        //    "nbf": 1702402602,
        //    "exp": 1702402902,
        //    "iss": "https://localhost:7193",
        //    "aud": "my_client_id_mvc",
        //    "nonce": "638379993944449073.MTI1MjU1YTMtZWU4MC00N2Q3LTkwODUtZWQzOWFmZTM0ZjIxNTUyNDJlYzItNTI3YS00YjAyLWE3ZTctNTk4OTcwYTgzMWE0",
        //    "iat": 1702402602,
        //    "at_hash": "g2NPztmgHE4QlV9CIzi3sg",
        //    "s_hash": "Wt5afo7Deu0SBc-nIx9Jgg",
        //    "sid": "BA1FE236F042FE866433535919BF52EB",
        //    "sub": "76ff27db-76b3-42ff-b309-cc9d785eca48",
        //    "auth_time": 1702402602,
        //    "idp": "local",
        //    "mytest.myvalue": "big.cookie", // LOOK AT THIS! This is my custom claim.
        //    "preferred_username": "bob",
        //    "name": "bob", // LOOK AT THIS! One more claim which was added to the id_token.
        //    "amr": [
        //        "pwd"
        //    ]
        //}
        var idToken = await HttpContext.GetTokenAsync("id_token");

        //{
        //    "nbf": 1702360005, // When the token starts to being valid.
        //    "exp": 1702363605, // When the token expires.
        //    "iss": "https://localhost:7193", // Who issued the token.
        //    "client_id": "my_client_id_mvc",
        //    "sub": "835c88ee-977e-42cf-9027-10182c27fbb2", // Basically the id of our user.
        //    "auth_time": 1702360005,
        //    "idp": "local",
        //    "jti": "ABBD042C267EFA00E962C30DCF287B81",
        //    "sid": "625D897A05BDE6AF274E94ED603C405F",
        //    "iat": 1702360005,
        //    "scope": [ // The scope we requested.
        //        "openid",
        //        "profile"
        //    ],
        //    "amr": [ // Authentication methods.
        //        "pwd"
        //    ]
        //}

        //// *** Access token after adding and requesting the new scope ***
        //{
        //    "nbf": 1702402143,
        //    "exp": 1702405743,
        //    "iss": "https://localhost:7193",
        //    "client_id": "my_client_id_mvc",
        //    "sub": "c3f1b580-de02-4666-a571-abecb3e534ae",
        //    "auth_time": 1702402143,
        //    "idp": "local",
        //    "jti": "08F3ADFB2AFDF7913A15A7E3B7B782D2",
        //    "sid": "203D428E7D402866A496ABF355382984",
        //    "iat": 1702402143,
        //    "scope": [
        //        "openid",
        //        "profile",
        //        "mytest.scope" // The new scope I requested.
        //    ],
        //    "amr": [
        //        "pwd"
        //    ]
        //}

        // NOTE: I have added 'config.GetClaimsFromUserInfoEndpoint = true'
        // in the Program.cs. It added some claims to my access_token,
        // but I expected to see more claims. We need to configure mapping
        // between the user endpoint and cookie.
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

        var secretCategories = await GetSecretCategoriesFromCatalog(accessToken);

        return View(secretCategories);
    }

    private async Task<IList<Models.Category>> GetSecretCategoriesFromCatalog(string accessToken, CancellationToken cancellationToken = default)
    {
        using var catalogApiClient = _httpClientFactory.CreateClient();
        catalogApiClient.SetBearerToken(accessToken);

        var requestUri = "http://localhost:5000/api/v1/categories?pageNumber=1&pageSize=5";
        var response = await catalogApiClient.GetAsync(requestUri, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(
                $"Failed to get data from the protected API. URL: '{requestUri}'. " +
                $"ReasonPhrase: {response.ReasonPhrase}. " +
                $"AccessToken: '{accessToken}'.");
        }

        var responseData = await response.Content.ReadAsStringAsync(cancellationToken);
        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Category>>(responseData);
    }
}