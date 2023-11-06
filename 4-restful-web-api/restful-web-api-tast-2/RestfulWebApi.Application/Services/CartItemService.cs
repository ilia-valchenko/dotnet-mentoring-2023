using RestfulWebApi.Application.DTOs;
using RestfulWebApi.Application.Services.Interfaces;

namespace RestfulWebApi.Application.Services
{
    public class CartItemService : ICartItemService
    {
        public Task<CartItem> CreateAsync(Guid cartId, CreateCartItem cartItemToCreate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
