using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LayeredArchitecture.UseCase.DTOs;

namespace LayeredArchitecture.UseCase.Services.Interfaces
{
    public interface IService<T> where T : BaseDto
    {
        Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<T>> GetAsync(CancellationToken cancellationToken = default);
        Task CreateAsync(T item, CancellationToken cancellationToken = default);
        Task UpdateAsync(T item, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
