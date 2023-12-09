using Microsoft.AspNetCore.Mvc;

namespace RestfulWebApi.Api.Controllers;

public class OAuthController : BaseController
{
    [HttpGet]
    public IActionResult Authorize()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Authorize(string username)
    {
        return View(0);
    }
}