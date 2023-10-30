using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestfulWebApi.Infrastructure.Options;

namespace RestfulWebApi.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Domain.Entities.Category>
    {
        private readonly IMapper _mapper;

        public CategoryRepository(IMapper mapper, IOptions<DataAccess> dataAccessOptions) : base(dataAccessOptions)
        {
            _mapper = mapper;
        }

        public override async Task<Domain.Entities.Category> CreateAsync(Domain.Entities.Category category, CancellationToken cancellationToken = default)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                int numberOfAffectedRows = await connection.ExecuteAsync(
                    "INSERT INTO Category (Id, Name, ImageUrl, ParentCategoryId) VALUES (@Id, @Name, @ImageUrl, @ParentCategoryId);",
                    new
                    {
                        Id = category.Id,
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
                return category;
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

        public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(connectionString);
            var numberOfAffectedRows = await connection.ExecuteAsync("DELETE FROM Category WHERE Id = @Id;", new { Id = id.ToString() });

            if (numberOfAffectedRows < 1)
            {
                throw new Exception($"Failed to delete the category. Id: '{id.ToString()}'.");
            }
        }

        public override async Task<Domain.Entities.Category> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            using var connection = new SqliteConnection(connectionString);
            var categories = await connection.QueryAsync<Infrastructure.Entities.Category>("SELECT * FROM Category WHERE Id = @Id;", new { Id = id.ToString() });
            return _mapper.Map<Domain.Entities.Category>(categories.SingleOrDefault());
        }

        public override async Task<IList<Domain.Entities.Category>> GetAsync(CancellationToken cancellationToken = default)
        {
            using var connection = new SqliteConnection(connectionString);
            var categories = await connection.QueryAsync<Infrastructure.Entities.Category>("SELECT * FROM Category;");
            return _mapper.Map<IList<Domain.Entities.Category>>(categories);
        }

        public override async Task UpdateAsync(Domain.Entities.Category category, CancellationToken cancellationToken = default)
        {
            using var connection = new SqliteConnection(connectionString);

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
