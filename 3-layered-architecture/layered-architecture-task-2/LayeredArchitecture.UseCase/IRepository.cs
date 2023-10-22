using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LayeredArchitecture.Domain.Entities;

namespace LayeredArchitecture.UseCase
{
    public interface IRepository
    {
        Task<Category> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<Category>> GetAsync(CancellationToken cancellationToken = default);
        Task CreateAsync(Category category, CancellationToken cancellationToken = default);
        Task UpdateAsync(Category category, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
