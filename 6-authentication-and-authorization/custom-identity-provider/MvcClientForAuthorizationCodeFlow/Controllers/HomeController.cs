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
    public IActionResult Secret()
    {
        return View();
    }
}