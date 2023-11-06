using RestfulWebApi.Application.DTOs;

namespace RestfulWebApi.Application;

public interface IRepository
{
    Task<IList<Cart>> GetAllCartsAsync(CancellationToken cancellationToken = default);
    Task<Cart?> GetCartByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Cart> CreateCartAsync(Cart cartToCreate, CancellationToken cancellationToken = default);
    Task<IList<CartItem>> GetAllCartsItemsAsync(CancellationToken cancellationToken = default);
    Task<CartItem?> GetCartItemByIdAsync(Guid cartId, Guid id, CancellationToken cancellationToken = default);
    Task<CartItem> CreateCartItemAsync(Guid cartId, CartItem cartItemToCreate, CancellationToken cancellationToken = default);
    Task DeleteCartItemAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default);
}