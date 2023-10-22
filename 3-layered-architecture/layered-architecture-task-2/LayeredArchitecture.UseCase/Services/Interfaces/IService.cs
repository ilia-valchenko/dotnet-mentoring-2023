using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LayeredArchitecture.UseCase.DTOs;

namespace LayeredArchitecture.UseCase.Services.Interfaces
{
    public interface IService<T> where T : BaseDto
    {
        Task<T> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<IList<T>> GetAsync(CancellationToken cancellationToken);
        Task CreateAsync(T item, CancellationToken cancellationToken);
        Task UpdateAsync(T item, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
