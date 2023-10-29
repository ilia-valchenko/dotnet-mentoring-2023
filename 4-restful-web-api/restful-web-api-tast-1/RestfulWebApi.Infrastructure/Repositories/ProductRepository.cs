using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestfulWebApi.Domain.Entities;
using RestfulWebApi.Infrastructure.Options;
using RestfulWebApi.UseCase;

namespace RestfulWebApi.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(IOptions<DataAccess> dataAccessOptions) : base(dataAccessOptions) { }

        public override async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            int numberOfAffectedRows = await connection.ExecuteAsync(
                "INSERT INTO Product (Id, Name, ImageUrl, Description, Price, Amount) VALUES (@Id, @Name, @ImageUrl, @Description, @Price, @Amount);",
                new
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    ImageUrl = product.ImageUrl?.UrlText,
                    Description = product.Description,
                    Price = product.Price,
                    Amount = product.Amount
                });

            if (numberOfAffectedRows < 1)
            {
                throw new Exception("Failed to insert the new product.");
            }

            return product;
        }

        public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            using var connection = new SqliteConnection(connectionString);
            var numberOfAffectedRows = await connection.ExecuteAsync("DELETE FROM Product WHERE Id = @Id;", new { Id = id.ToString() });

            if (numberOfAffectedRows < 1)
            {
                throw new Exception($"Failed to delete the product. Id: '{id.ToString()}'.");
            }
        }

        public override async Task<Product> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            using var connection = new SqliteConnection(connectionString);
            var products = await connection.QueryAsync<Product>("SELECT * FROM Product WHERE Id = @Id;", new { Id = id.ToString() });
            return products.SingleOrDefault();
        }

        public override async Task<IList<Product>> GetAsync(CancellationToken cancellationToken = default)
        {
            using var connection = new SqliteConnection(connectionString);
            var products = await connection.QueryAsync<Product>("SELECT * FROM Product;");
            return products.ToList();
        }

        public override async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            using var connection = new SqliteConnection(connectionString);

            var numberOfAffectedRows = await connection.ExecuteAsync(
                "UPDATE Product SET CategoryId = @CategoryId, Name = @Name, ImageUrl = @ImageUrl, Description = @Description, Price = @Price, Amount = @Amount WHERE Id = @Id;",
                new
                {
                    Id = product.Id.ToString(),
                    CategoryId = product.CategoryId.ToString(),
                    Name = product.Name,
                    ImageUrl = product.ImageUrl?.UrlText,
                    Description = product.Description,
                    Price = product.Price,
                    Amount = product.Amount
                });

            if (numberOfAffectedRows < 1)
            {
                throw new Exception(
                    "Failed to update the product. " +
                    $"Serialized product: {JsonConvert.SerializeObject(product)}.");
            }
        }
    }
}
