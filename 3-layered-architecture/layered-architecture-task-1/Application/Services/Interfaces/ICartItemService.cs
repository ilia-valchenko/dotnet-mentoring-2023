using Application.DTOs;

namespace Application.Services.Interfaces
{
    public interface ICartItemService
    {
        public Task AddItemAsync(Guid cartId, CartItemDto itemToAdd, CancellationToken cancellationToken = default);
        public Task RemoveItemAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default);
    }
}
