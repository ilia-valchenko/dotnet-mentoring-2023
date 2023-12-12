using System.Security.Cryptography;
using IdentityServer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static IdentityModel.OidcConstants;
using static IdentityServer4.Models.IdentityResources;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(
        Microsoft.AspNetCore.Identity.SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

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
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        // 1. Check if the model is valid.
        // Here we will redirect back to whatever the redirect URL is.

        // You might wondering what is persistent is.
        // * 'isPersistent'*  is a persistent cookie which is essentially is
        // going to persist in the browser. So, usually when we close the browser
        // cookies are get deleted. Persistent cookie stays in the browser and
        // survives the closure of the browser. So I start my browser again my
        // sign-in session still there.
        // * 'lockoutOnFailure'* you can configure the amount of attempts you get
        // to login. If we set it to true assentially after like three attempts
        // we might get locked out.
        var signInResult = await _signInManager.PasswordSignInAsync(
            loginViewModel.Username,
            loginViewModel.Password,
            false, 
            false);

        if (signInResult.Succeeded)
        {
            // Here we just want to redirect back to the Return URL.

            // /connect/authorize/callback?client_id=my_client_id_mvc
            //&redirect_uri=https%3A%2F%2Flocalhost%3A7240%2Fsignin-oidc
            //&response_type=code
            //&scope=openid%20profile
            //&code_challenge=T2_qg1d5f31Pzlk0l8ucHv4nFlQWwVuD4-qLyaeOpVg
            //&code_challenge_method=S256
            //&response_mode=form_post
            //&nonce=638379531381212979.YzliMTZhMjItNjdiNy00MmEyLWFjZmMtMj
            //&state=CfDJ8Gbrtnk6qZFAsrdxVOBx3dUOCuA-ivMTqhwUA-oRNQKL
            return Redirect(loginViewModel.ReturnUrl);
        }
        else if (signInResult.IsLockedOut)
        {
            // Here we can send an email with the recovery for example.
            throw new Exception("You are locked out!");
        }

        return View();
    }
}