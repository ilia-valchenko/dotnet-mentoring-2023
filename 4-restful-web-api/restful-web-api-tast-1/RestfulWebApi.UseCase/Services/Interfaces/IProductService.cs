using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RestfulWebApi.UseCase.DTOs;

namespace RestfulWebApi.UseCase.Services.Interfaces
{
    public interface IProductService : IService<Product>
    {
        Task<IList<Product>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
    }
}
