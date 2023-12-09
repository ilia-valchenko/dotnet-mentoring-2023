using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DummyIdentityServer.Web.Controllers;

public class OAuthController : Controller
{
    private const int CodeSize = 10;

    [HttpGet]
    public IActionResult Authorize(
        string response_type, // Authorization flow type.
        string client_id,
        string redirect_uri,
        string scope, // What information I request. Example: email, tel.
        string state) // Random string generated to confirm that we are going to back to the same client.
                      // The client generate this state string. If the state changes once we go back to our
                      // client this is essentially going to error that basically we haven't requested this authorization.
    {
        //?a=foo&b=bar
        var query = new QueryBuilder();
        query.Add("redirectUri", redirect_uri);
        query.Add("state", state);

        return View(model: query.ToString());
    }

    [HttpPost]
    public IActionResult Authorize(
        string username,
        string redirectUri,
        string state) // The state has to persist once we go back in order to confirm
                      // that is coming back from the correct authorization endpoint.
    {
        var code = GenerateRandomString(CodeSize);

        return Redirect("");
    }

    public IActionResult Token()
    {
        return View();
    }

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