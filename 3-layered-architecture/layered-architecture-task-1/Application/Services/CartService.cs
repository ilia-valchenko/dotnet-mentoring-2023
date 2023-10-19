using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CartDto cart, CancellationToken cancellationToken = default)
    {
        var cartModel = _mapper.Map<Cart>(cart);
        await _cartRepository.CreateAsync(cartModel, cancellationToken);
    }

    public async Task<IList<CartDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        var models = await _cartRepository.GetAsync(cancellationToken);
        return _mapper.Map<IList<CartDto>>(models);
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
    }
}