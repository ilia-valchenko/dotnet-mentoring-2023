using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestfulWebApi.UseCase.Services.Interfaces
{
    public interface IProductService : IService<UseCase.DTOs.Product>
    {
        Task<IList<UseCase.DTOs.Product>> GetByCategoryIdAsync(Guid categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<UseCase.DTOs.Product> CreateAsync(UseCase.DTOs.CreateProduct productToCreate, CancellationToken cancellationToken = default);
        Task<UseCase.DTOs.Product> UpdateAsync(Guid id, UseCase.DTOs.UpdateProduct productToUpdate, CancellationToken cancellationToken = default);
    }
}
