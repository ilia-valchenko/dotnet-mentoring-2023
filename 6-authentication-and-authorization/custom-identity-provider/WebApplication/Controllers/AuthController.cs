using System.Security.Cryptography;
using IdentityServer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static IdentityModel.OidcConstants;
using static IdentityServer4.Models.IdentityResources;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.WebRequestMethods;

namespace IdentityServer.Controllers;

public class AuthController : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        // *** Example of the Return URL ***
        // /connect/authorize/callback?client_id=my_client_id_mvc
        //&redirect_uri=https%3A%2F%2Flocalhost%3A7240%2Fsignin-oidc
        //&response_type=code
        //&scope=openid%20profile
        //&code_challenge=74ut48tsVsebW9k9UAiR8YJK0NitTONmlEFyqScCAAw
        //&code_challenge_method=S256
        //&response_mode=form_post
        //&nonce=638379231249474333.NDk5YjM3MGEtZTgwMy00Y2JjLWI0NDYtOGYzZGY4ZDZjZjRkOGU1
        //&state=CfDJ8Gbrtnk6qZFAsrdxVOBx3dVok4BKle1zRKgTVU9LBpU8dWEDgdRVjkRRSTPotbmM0ve

        return View(new LoginViewModel
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel loginViewModel)
    {
        // Here we will redirect back to whatever the redirect URL is.
        return View();
    }
}