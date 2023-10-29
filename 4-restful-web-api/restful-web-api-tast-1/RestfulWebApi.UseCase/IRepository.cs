using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RestfulWebApi.Domain.Entities;

namespace RestfulWebApi.UseCase
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<T>> GetAsync(CancellationToken cancellationToken = default);
        Task<T> CreateAsync(T itemToCreate, CancellationToken cancellationToken = default);
        Task UpdateAsync(T itemToUpdate, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
