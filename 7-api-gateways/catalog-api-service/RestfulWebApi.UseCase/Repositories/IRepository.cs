using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RestfulWebApi.Domain.Entities;

namespace RestfulWebApi.UseCase.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<T> CreateAsync(T itemToCreate, CancellationToken cancellationToken = default);
        Task UpdateAsync(T itemToUpdate, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
