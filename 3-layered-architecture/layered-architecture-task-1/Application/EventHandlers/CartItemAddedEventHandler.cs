using Application.DTOs;
using Application.Services.Interfaces;
using Domain.Events;
using MediatR;

namespace Application.EventHandlers
{
    public class CartItemAddedEventHandler : INotificationHandler<CartItemAdded>
    {
        private readonly ICartService _cartService;

        public CartItemAddedEventHandler(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task Handle(CartItemAdded notification, CancellationToken cancellationToken)
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

            var updateCart = new UpdateCartDto { Quantity = cart.Quantity + 1 };
            await _cartService.UpdateAsync(cart.Id, updateCart, cancellationToken);
        }
    }
}
