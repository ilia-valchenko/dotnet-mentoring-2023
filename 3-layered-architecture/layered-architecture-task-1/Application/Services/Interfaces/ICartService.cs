using Application.DTOs;

namespace Application.Services.Interfaces;

public interface ICartService
{
    public Task<IList<CartDto>> GetAsync(CancellationToken cancellationToken = default);
    public Task<CartDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task CreateAsync(CartDto cart, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Guid id, UpdateCartDto cartToUpdate, CancellationToken cancellationToken = default);
}
