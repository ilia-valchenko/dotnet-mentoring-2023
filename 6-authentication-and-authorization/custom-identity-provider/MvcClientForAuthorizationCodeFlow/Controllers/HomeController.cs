using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcClientForAuthorizationCodeFlow.Controllers;

public class HomeController : Controller
{
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
    [Authorize]
    public async Task<IActionResult> Secret()
    {
        // Let's try to extract
        // - id_token
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
        var idToken = await HttpContext.GetTokenAsync("id_token");

        var _idToken = new JwtSecurityTokenHandler().ReadJwtToken(idToken);

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
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

        return View();
    }
}