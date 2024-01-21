using MessageBasedArchitecture.Application.Models;

namespace MessageBasedArchitecture.Application.Services.Interfaces;

public interface IService
{
    Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(Guid id, UpdateItem updateItem, CancellationToken cancellationToken = default);
}