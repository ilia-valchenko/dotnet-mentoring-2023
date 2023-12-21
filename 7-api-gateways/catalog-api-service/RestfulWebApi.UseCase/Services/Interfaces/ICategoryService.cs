using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestfulWebApi.UseCase.Services.Interfaces
{
    public interface ICategoryService : IService<UseCase.DTOs.Category>
    {
        Task<UseCase.DTOs.Category> CreateAsync(UseCase.DTOs.CreateCategory categoryToCreate, CancellationToken cancellationToken = default);
        Task<UseCase.DTOs.Category> UpdateAsync(Guid id, UseCase.DTOs.UpdateCategory categoryToUpdate, CancellationToken cancellationToken = default);
    }
}
