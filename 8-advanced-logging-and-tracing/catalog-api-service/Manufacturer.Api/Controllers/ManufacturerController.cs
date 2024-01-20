using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manufacturer.Api.Controllers;

[Route("api/v1")]
[ApiController]
public class ManufacturerController : ControllerBase
{
    [HttpGet("manufacturers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Models.Manufacturer>))]
    //[Authorize(Roles = "manager, buyer")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        Serilog.Log.Information("[Manufacturer API] Starting to get a list of all manufacturers.");

        var claims = User.Claims.ToList();
        var manufacturers = StaticData.StaticData.GetAllManufacturer();
        return Ok(manufacturers);
    }

    [HttpGet("manufacturers/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Manufacturer))]
    //[Authorize(Roles = "manager, buyer")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Serilog.Log.Information($"[Manufacturer API] Starting to get a single manufacturer by id: '{id.ToString()}'.");

        var claims = User.Claims.ToList();
        var manufacturer = StaticData.StaticData.GetManufacturerById(id);
        return Ok(manufacturer);
    }
}