using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LayeredArchitecture.Domain.Entities;

namespace LayeredArchitecture.UseCase
{
    public interface IRepository
    {
        Task<Category> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<IList<Category>> GetAsync(CancellationToken cancellationToken);
        Task CreateAsync(Category category, CancellationToken cancellationToken);
        Task UpdateAsync(Category category, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
