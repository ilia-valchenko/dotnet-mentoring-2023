using Application.Common.Interfaces;
using Application.DTOs;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository cartRepository;
    private readonly ILogger<CartService> logger;

    public CartService(ICartRepository cartRepository, ILogger<CartService> logger)
    {
        this.cartRepository = cartRepository;
        this.logger = logger;
    }

    public Task AddAsync(Guid cartId, CartItemDto itemToAdd, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(CartDto cart, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IList<CartDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}