using MessageBasedArchitecture.Domain.Entities;

namespace MessageBasedArchitecture.Application;

public interface IRepository
{
    Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(Item item, CancellationToken cancellationToken = default);
}
