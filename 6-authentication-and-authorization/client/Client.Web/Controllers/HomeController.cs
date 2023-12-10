﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Secret()
    {
        // Authenticates the request using the default authentication scheme and returns the value for the token.
        // BTW We will see nothing until we set config.SaveTokens = true in Startup.
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        // Now we have an access token. It means that we can use it for calling multiple APIs that can be protected.
        // Primarilly the idea for using this access token is essentially including the request
        // to an API the same way what we did with Postman (via Header or URL).
        // We will send these requests to the protected APIs and then
        // these APIs are gonna go to the IdentityServer and make sure
        // that the tokens are valid.
        // It's like a triangle communication.

        return View();
    }

    //// *** ResponseJson value ***
    //{
    //    "access_token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzb21lX2lkIiwibXlfY3VzdG9tX2NsYWltIjoiY3VzdG9tX2NsYWltX3ZhbHVlIiwibmJmIjoxNzAyMTk5MjUzLCJleHAiOjE3MDIyMDI4NTMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzY3LyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8ifQ.EZuSBfuKrpG6NKsHFcd6ZQzRlCyCrGhzHDlmKUu-D3U",
    //    "token_type":"Bearer",
    //    "raw_claim":"oauthTutorial"
    //}

    //// *** Decoded access_token ***
    //{
    //    "sub": "some_id",
    //    "my_custom_claim": "custom_claim_value",
    //    "nbf": 1702199253,
    //    "exp": 1702202853,
    //    "iss": "https://localhost:44367/",
    //    "aud": "http://localhost:5000/"
    //}
}