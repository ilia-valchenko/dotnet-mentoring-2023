using Microsoft.AspNetCore.Mvc;
using RestfulWebApi.Application.DTOs;
using RestfulWebApi.Application.Services.Interfaces;

namespace RestfulWebApi.Web.Controllers
{
    public class CartsController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartsController"/> class.
        /// </summary>
        /// <param name="cartService">The cart service.</param>
        /// <param name="logger">The logger.</param>
        public CartsController(ICartService cartService, ILogger<CartsController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [HttpGet("v1/carts/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cart))]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cart = await _cartService.GetByIdAsync(id, cancellationToken);
            return Ok(cart);
        }
    }
}
