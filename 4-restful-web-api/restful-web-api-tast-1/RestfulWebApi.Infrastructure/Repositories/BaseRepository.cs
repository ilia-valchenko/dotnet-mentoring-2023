using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RestfulWebApi.Domain.Entities;
using RestfulWebApi.Infrastructure.Options;
using RestfulWebApi.UseCase;

namespace RestfulWebApi.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly string connectionString;

        protected BaseRepository(IOptions<DataAccess> dataAccessOptions)
        {
            if (string.IsNullOrWhiteSpace(dataAccessOptions?.Value?.ConnectionString))
            {
                throw new ArgumentException("Data access options do not contain a security string or the security string is invalid.");
            }

            connectionString = dataAccessOptions.Value.ConnectionString;
        }

        public abstract Task<T> CreateAsync(T itemToCreate, CancellationToken cancellationToken = default);

        public abstract Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        public abstract Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default);

        public abstract Task<IList<T>> GetAsync(CancellationToken cancellationToken = default);

        public abstract Task UpdateAsync(T itemToUpdate, CancellationToken cancellationToken = default);
    }
}
