using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Events;
using Domain.Models;
using MediatR;

namespace Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartItemService(ICartRepository cartRepository, IMediator mediator, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task AddItemAsync(Guid cartId, CartItemDto itemToAdd, CancellationToken cancellationToken = default)
        {
            // TODO: We can do it in a different way.
            // We can add Add(CartItem) method to the Cart domain model.
            // In this case we will retrieve an instance of a cart model from the repository.
            // Will call the new Add(CartItem) and will update the whole instance of the domain model.
            // Example:
            // var cartModel = await _cartRepository.GetAsync(id, cancellationToken);
            // cartModel.Add(itemToAdd);
            // await _cartRepository.UpdateAsync(cartModel, cancellationToken);

            var cartItemModel = _mapper.Map<CartItem>(itemToAdd);
            await _cartRepository.AddItemAsync(cartId, cartItemModel, cancellationToken);
            await _mediator.Publish(new CartItemAdded(cartId), cancellationToken);
        }

        public async Task RemoveItemAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default)
        {
            // TODO: We can do it in a different way.
            // We can add Remove(CartItem) method to the Cart domain model.
            // In this case we will retrieve an instance of a cart model from the repository.
            // Will call the new Remove(CartItem) and will update the whole instance of the domain model.
            // Example:
            // var cartModel = await _cartRepository.GetAsync(id, cancellationToken);
            // cartModel.Remove(itemToRemove);
            // await _cartRepository.UpdateAsync(cartModel, cancellationToken);

            await _cartRepository.RemoveItemAsync(cartId, cartItemId, cancellationToken);
            await _mediator.Publish(new CartItemDeleted(cartId), cancellationToken);
        }
    }
}
