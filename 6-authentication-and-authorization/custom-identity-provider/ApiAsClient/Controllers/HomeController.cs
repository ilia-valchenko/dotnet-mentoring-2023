using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using ApiAsClient.Models;

namespace ApiAsClient.Controllers;

[Route("api/v1")]
public class HomeController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
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

        // *** <TokenResponse> type contains: ***
        // - AccessToken
        // - IdentityToken
        // - RefreshToken
        // - ErrorDescription
        // - ExpiresIn

        using var catalogApiClient = _httpClientFactory.CreateClient();
        catalogApiClient.SetBearerToken(tokenResponse.AccessToken);

        var requestUri = "http://localhost:5000/api/v1/categories?pageNumber=1&pageSize=5";
        var response = await catalogApiClient.GetAsync(requestUri, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get data from the protected API. URL: {requestUri}.");
        }

        var responseData = await response.Content.ReadAsStringAsync(cancellationToken);
        var deserializedData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Category>>(responseData);

        return Ok(new
        {
            access_token = tokenResponse.AccessToken,
            secured_data = deserializedData
        });
    }
}