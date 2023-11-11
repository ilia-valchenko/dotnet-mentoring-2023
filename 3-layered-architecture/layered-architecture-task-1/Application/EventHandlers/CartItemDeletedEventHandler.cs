using Application.DTOs;
using Application.Services.Interfaces;
using Domain.Events;
using MediatR;

namespace Application.EventHandlers
{
    public class CartItemDeletedEventHandler : INotificationHandler<CartItemDeleted>
    {
        private readonly ICartService _cartService;

        public CartItemDeletedEventHandler(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task Handle(CartItemDeleted notification, CancellationToken cancellationToken)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            var cart = await _cartService.GetByIdAsync(notification.CartId, cancellationToken);

            if (cart == null)
            {
                throw new Exception($"Failed to find a cart with id: '{notification.CartId}'.");
            }

            if (cart.Quantity == 0)
            {
                throw new Exception("The cart doesn't have any items.");
            }

            var updateCart = new UpdateCartDto { Quantity = cart.Quantity - 1 };
            await _cartService.UpdateAsync(cart.Id, updateCart, cancellationToken);
        }
    }
}
