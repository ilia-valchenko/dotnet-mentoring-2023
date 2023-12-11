using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;

namespace ApiAsClient.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        // 1. Retrieve an access token.
        // 2. Retrieve secret data.

        using var identityServerClient = _httpClientFactory.CreateClient();

        // This document contains all information we need about our IdentityServer.
        var discoveryDocument = await identityServerClient.GetDiscoveryDocumentAsync(
            "https://localhost:7193/",
            cancellationToken);

        var clientCredentialsTokenRequest = new ClientCredentialsTokenRequest
        {
            Address = discoveryDocument.TokenEndpoint,
            ClientId = Constants.Constants.ClientId,
            ClientSecret = Constants.Constants.ClientSecret,
            Scope = "catalog-api"
        };

        var tokenResponse = await identityServerClient.RequestClientCredentialsTokenAsync(
            clientCredentialsTokenRequest,
            cancellationToken);

        // *** <TokenResponse> type ***
        // - AccessToken
        // - IdentityToken
        // - RefreshToken
        // - ErrorDescription
        // - ExpiresIn

        return Ok(new
        {

        });
    }
}