using RestfulWebApi.Application.DTOs;
using RestfulWebApi.Application.Services.Interfaces;

namespace RestfulWebApi.Application.Services
{
    public class CartService : ICartService
    {
        public Task<Cart> CreateAsync(CreateCart cartToCreate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
