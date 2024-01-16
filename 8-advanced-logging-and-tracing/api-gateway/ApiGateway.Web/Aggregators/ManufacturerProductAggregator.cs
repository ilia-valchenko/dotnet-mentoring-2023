using System.Net;
using System.Net.Http.Headers;
using ApiGateway.Web.DTOs;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace ApiGateway.Web.Aggregators;

public class ManufacturerProductAggregator : IDefinedAggregator
{
    private readonly ILogger<ManufacturerProductAggregator> _logger;

    public ManufacturerProductAggregator(ILogger<ManufacturerProductAggregator> logger)
    {
        _logger = logger;
    }

    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
    {
        _logger.LogInformation("$$$$$$$$$$$$$$$$$$$$$$$$$ Start doing aggregation $$$$$$$$$$$$$$$$$$$$$$$$$");

        var manufacturers = await responses[0].Items.DownstreamResponse().Content.ReadFromJsonAsync<List<Manufacturer>>();
        var products = await responses[1].Items.DownstreamResponse().Content.ReadFromJsonAsync<List<Product>>();

        if (manufacturers == null)
        {
            manufacturers = new List<Manufacturer>();
        }
        else if (products != null && products.Any())
        {
            foreach (var manufacturer in manufacturers)
            {
                manufacturer.Products = products.Where(p => p.ManufacturerId == manufacturer.Id);
            }
        }

        var jsonString = JsonConvert.SerializeObject(manufacturers, Formatting.Indented);

        var stringContent = new StringContent(jsonString)
        {
            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
        };

        return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
    }
}