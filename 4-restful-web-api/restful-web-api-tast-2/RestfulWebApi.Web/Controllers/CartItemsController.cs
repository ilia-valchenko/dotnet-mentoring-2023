using Microsoft.AspNetCore.Mvc;
using RestfulWebApi.Application.DTOs;
using RestfulWebApi.Application.Services.Interfaces;

namespace RestfulWebApi.Web.Controllers
{
    public class CartItemsController : BaseController
    {
        private readonly ICartItemService _cartItemService;
        private readonly ILogger<CartsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartItemsController"/> class.
        /// </summary>
        /// <param name="cartService">The cart service.</param>
        /// <param name="logger">The logger.</param>
        public CartItemsController(ICartItemService cartItemService, ILogger<CartsController> logger)
        {
            _cartItemService = cartItemService;
            _logger = logger;
        }

        [HttpGet("v1/carts/{cartId:Guid}/items/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cart))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid cartId, Guid id, CancellationToken cancellationToken = default)
        {
            var cart = await _cartItemService.GetByIdAsync(cartId, id, cancellationToken);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost("v1/carts/{cartId:Guid}/items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAsync(Guid cartId, CreateCartItem cartItemToCreate, CancellationToken cancellationToken = default)
        {
            var createdCartItem = await _cartItemService.CreateAsync(cartId, cartItemToCreate, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdCartItem.Id }, createdCartItem);
        }
    }
}
