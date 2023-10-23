using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LayeredArchitecture.Domain.Entities;
using LayeredArchitecture.UseCase;

namespace LayeredArchitecture.Infrastructure
{
    public class ProductRepository : IRepository<Product>
    {
        public Task CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Product>> GetAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
