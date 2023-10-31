using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RestfulWebApi.UseCase.DTOs;

namespace RestfulWebApi.UseCase.Services.Interfaces
{
    public interface IService<T> where T : BaseDto
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<T> CreateAsync(T item, CancellationToken cancellationToken = default);
        Task UpdateAsync(T item, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
