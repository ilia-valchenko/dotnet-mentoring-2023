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

        [HttpGet("v1/carts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Cart>))]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var carts = await _cartService.GetAllAsync(cancellationToken);
            return Ok(carts);
        }

        [HttpGet("v1/carts/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cart))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cart = await _cartService.GetByIdAsync(id, cancellationToken);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpGet("v2/carts/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<CartItem>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCartInfoByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cart = await _cartService.GetByIdAsync(id, cancellationToken);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart.Items);
        }

        [HttpPost("v1/carts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(CreateCart cartToCreate, CancellationToken cancellationToken = default)
        {
            var createdCart = await _cartService.CreateAsync(cartToCreate, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdCart.Id }, createdCart);
        }
    }
}
