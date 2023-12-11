using IdentityServer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers;

public class AuthController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Login(LoginViewModel loginViewModel)
    {
        // Here we will redirect back to whatever the redirect URL is.
        return View();
    }
}