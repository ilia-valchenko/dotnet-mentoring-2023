using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using LayeredArchitecture.Domain.Entities;
using LayeredArchitecture.UseCase;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace LayeredArchitecture.Infrastructure
{
    public class Repository : IRepository
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task CreateAsync(Category category, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(_connectionString);
            var numberOfAffectedRows = await connection.ExecuteAsync("DELETE FROM Category WHERE Id = @Id", new { Id = id.ToString() });

            if (numberOfAffectedRows < 1)
            {
                throw new Exception($"Failed to delete the category. CategoryId: '{id.ToString()}'.");
            }
        }

        public async Task<Category> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(_connectionString);
            var tours = await connection.QueryAsync<Category>("SELECT * FROM Category WHERE Id = @Id", new { Id = id.ToString() });
            return tours.FirstOrDefault();
        }

        public async Task<IList<Category>> GetAsync(CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(_connectionString);
            var tours = await connection.QueryAsync<Category>("SELECT * FROM Category");
            return tours.ToList();
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(_connectionString);

            var numberOfAffectedRows = await connection.ExecuteAsync(
                "UPDATE Category SET Name = @Name, ImageUrl = @ImageUrl, ParentCategoryId = @ParentCategoryId WHERE Id = @Id",
                new {
                    Id = category.Id.ToString(),
                    Name = category.Name,
                    ImageUrl = category.ImageUrl?.UrlText,
                    ParentCategoryId = category.ParentCategoryId.ToString()
                });

            if (numberOfAffectedRows < 1)
            {
                throw new Exception(
                    "Failed to update the category. " +
                    $"Serialized category: {JsonConvert.SerializeObject(category)}.");
            }
        }
    }
}
