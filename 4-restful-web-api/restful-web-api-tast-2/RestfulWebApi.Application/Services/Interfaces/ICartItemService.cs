using RestfulWebApi.Application.DTOs;

namespace RestfulWebApi.Application.Services.Interfaces;

public interface ICartItemService : IService<CartItem>
{
    Task<CartItem> CreateAsync(Guid cartId, CreateCartItem cartItemToCreate, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default);
}