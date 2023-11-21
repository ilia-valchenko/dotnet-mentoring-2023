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
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IService service, ILogger<ItemsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Item>))]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var items = await _service.GetAllAsync(cancellationToken);
            return Ok(items);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllAsync(Guid id, UpdateItem updateItem, CancellationToken cancellationToken = default)
        {
            try
            {
                await _service.UpdateAsync(id, updateItem, cancellationToken);
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