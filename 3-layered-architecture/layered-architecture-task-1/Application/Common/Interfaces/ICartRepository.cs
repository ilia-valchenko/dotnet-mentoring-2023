using Domain.Models;

namespace Application.Common.Interfaces;

public interface ICartRepository
{
    public Task<IList<Cart>> GetAsync(CancellationToken cancellationToken = default);
    public Task CreateAsync(Cart cart, CancellationToken cancellationToken = default);
    public Task AddItemAsync(Guid cartId, CartItem itemToAdd, CancellationToken cancellationToken = default);
    public Task RemoveItemAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default);
}