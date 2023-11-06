using RestfulWebApi.Application.DTOs;

namespace RestfulWebApi.Application.Services.Interfaces
{
    public interface ICartService : IService<Cart>
    {
        Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Cart> CreateAsync(CreateCart cartToCreate, CancellationToken cancellationToken = default);
    }
}
