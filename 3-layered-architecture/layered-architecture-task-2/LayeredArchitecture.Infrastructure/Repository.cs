using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LayeredArchitecture.Domain.Entities;
using LayeredArchitecture.UseCase;

namespace LayeredArchitecture.Infrastructure
{
    public class Repository : IRepository
    {
        public Task CreateAsync(Category category, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Category>> GetAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
