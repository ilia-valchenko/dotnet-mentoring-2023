using Application.DTOs;

namespace Application.Common.Interfaces;

public interface ICartService
{
    public Task<IList<CartDto>> GetAsync(CancellationToken cancellationToken = default);
    public Task CreateAsync(CartDto cart, CancellationToken cancellationToken = default);
    public Task AddItemAsync(Guid cartId, CartItemDto itemToAdd, CancellationToken cancellationToken = default);
    public Task RemoveItemAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default);
}
