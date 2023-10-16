using Application.DTOs;

namespace Application.Common.Interfaces;

public interface ICartService
{
    public Task<IList<CartDto>> GetAsync(CancellationToken cancellationToken = default);
    public Task CreateAsync(CartDto cart, CancellationToken cancellationToken = default);
    public Task AddAsync(Guid cartId, CartItemDto itemToAdd, CancellationToken cancellationToken = default);
    public Task RemoveAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default);
}
