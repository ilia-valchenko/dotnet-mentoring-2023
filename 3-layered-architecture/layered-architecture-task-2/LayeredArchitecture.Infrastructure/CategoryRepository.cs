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
    public class CategoryRepository : IRepository<Category>
    {
        private readonly string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreateAsync(Category category, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                int numberOfAffectedRows = await connection.ExecuteAsync(
                    "INSERT INTO Category (Id, Name, ImageUrl, ParentCategoryId) VALUES (@Id, @Name, @ImageUrl, @ParentCategoryId);",
                    new
                    {
                        Id = category.Id.ToString(),
                        Name = category.Name,
                        ImageUrl = category.ImageUrl?.UrlText,
                        ParentCategoryId = category.ParentCategoryId?.ToString()
                    },
                    transaction);

                // Insert related objects.
                foreach (var product in category.Products)
                {
                    numberOfAffectedRows += await connection.ExecuteAsync(
                    "INSERT INTO Product (Id, CategoryId, Name, Description, ImageUrl, Price, Amount) " +
                    "VALUES (@Id, @CategoryId, @Name, @Description, @ImageUrl, @Price, @Amount);",
                    new
                    {
                        Id = product.Id.ToString(),
                        CategoryId = product.CategoryId.ToString(),
                        Name = product.Name,
                        Description = product.Description,
                        ImageUrl = product.ImageUrl?.UrlText,
                        Price = product.Price,
                        Amount = product.Amount
                    },
                    transaction);
                }

                if (numberOfAffectedRows < category.Products.Count + 1)
                {
                    throw new Exception("Failed to insert the new category.");
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                throw new Exception(
                    "Failed to insert the new category. " +
                    $"Serialized category: {JsonConvert.SerializeObject(category)}.",
                    ex);
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(_connectionString);
            var numberOfAffectedRows = await connection.ExecuteAsync("DELETE FROM Category WHERE Id = @Id;", new { Id = id.ToString() });

            if (numberOfAffectedRows < 1)
            {
                throw new Exception($"Failed to delete the category. CategoryId: '{id.ToString()}'.");
            }
        }

        public async Task<Category> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(_connectionString);
            var tours = await connection.QueryAsync<Category>("SELECT * FROM Category WHERE Id = @Id;", new { Id = id.ToString() });
            return tours.FirstOrDefault();
        }

        public async Task<IList<Category>> GetAsync(CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(_connectionString);
            var tours = await connection.QueryAsync<Category>("SELECT * FROM Category;");
            return tours.ToList();
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(_connectionString);

            var numberOfAffectedRows = await connection.ExecuteAsync(
                "UPDATE Category SET Name = @Name, ImageUrl = @ImageUrl, ParentCategoryId = @ParentCategoryId WHERE Id = @Id;",
                new
                {
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
