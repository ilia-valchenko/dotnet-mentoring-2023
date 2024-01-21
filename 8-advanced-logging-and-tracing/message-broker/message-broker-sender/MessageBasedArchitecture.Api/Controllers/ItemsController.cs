using MessageBasedArchitecture.Application.Models;
using MessageBasedArchitecture.Application.Services.Interfaces;
using MessageBasedArchitecture.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MessageBasedArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/v1/items")]
    public class ItemsController : ControllerBase
    {
        private readonly IService _service;

        public ItemsController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Item>))]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            Serilog.Log.Information("[MessageSender API] Starting to get a list of all items.");

            var items = await _service.GetAllAsync(cancellationToken);
            return Ok(items);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Item>>> UpdateAsync(Guid id, UpdateItem updateItem, CancellationToken cancellationToken = default)
        {
            Serilog.Log.Information($"[MessageSender API] Starting to update existing item. Id: '{id.ToString()}'.");

            var correlationId = Guid.Empty;
            string correlationIdString = HttpContext.Request.Headers["x-correlation-id"];

            if (!string.IsNullOrWhiteSpace(correlationIdString))
            {
                correlationId = Guid.Parse(correlationIdString);
            }

            try
            {
                await _service.UpdateAsync(id, updateItem, correlationId, cancellationToken);
                return Ok();
            }
            catch (ValueNotValidException valueNotValidException)
            {
                return BadRequest(valueNotValidException.Message);
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
        }
    }
}