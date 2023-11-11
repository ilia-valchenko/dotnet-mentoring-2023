using Application.DTOs;
using Application.Services.Interfaces;
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

    public async Task<CartDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var model = await _cartRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<CartDto>(model);
    }

    public async Task UpdateAsync(Guid id, UpdateCartDto cartToUpdate, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetByIdAsync(id, cancellationToken);
        cart.Quantity = cartToUpdate.Quantity;
        await _cartRepository.UpdateAsync(cart, cancellationToken);
    }
}