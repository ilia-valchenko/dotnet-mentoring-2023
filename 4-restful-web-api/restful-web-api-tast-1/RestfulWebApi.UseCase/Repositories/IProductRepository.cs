using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestfulWebApi.UseCase.Repositories
{
    public interface IProductRepository : IRepository<Domain.Entities.Product>
    {
        Task<IList<Domain.Entities.Product>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
    }
}
