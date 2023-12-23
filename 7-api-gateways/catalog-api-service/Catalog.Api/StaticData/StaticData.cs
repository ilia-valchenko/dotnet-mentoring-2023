using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Api.Models;

namespace Catalog.Api.StaticData
{
    public static class StaticData
    {
        private const int NumberOfCategories = 5;
        private const int NumberOfProducts = 3;

        private static Guid[] _manufacturerIds = new[] {
            Guid.Parse("ba212e9c-8201-4725-8198-b26e0738ff6e"),
            Guid.Parse("619ee7e7-7869-4016-883e-26f5cc8549db"),
            Guid.Parse("0f4b9300-8ba8-4529-b0fb-7c19451c913c")
        };

        private static IList<Category> _categories = new List<Category>();

        static StaticData()
        {
            for (int i = 0; i < NumberOfCategories; i++)
            {
                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = $"Test category {i}",
                    ImageUrlText = $"https://img.freepik.com/free-photo/sweet-pastry-assortment-top-view_v{i}.jpg",
                    Products = new List<Product>()
                };

                for (int j = 0; j < NumberOfProducts; j++)
                {
                    var productNumber = i * j;

                    category.Products.Add(new Product
                    {
                        Id= Guid.NewGuid(),
                        CategoryId = category.Id,
                        ManufacturerId = GetRandomManufacturerId(),
                        Name = $"Test product {productNumber}",
                        Description = $"Test description for product {productNumber}",
                        Amount = j,
                        ImageUrlText = $"https://img.freepik.com/free-photo/sweet-pastry-assortment-top-view_v{j}.jpg",
                        Price = j
                    });
                }

                _categories.Add(category);
            }
        }

        public static IList<Category> GetAllCategories()
        {
            return _categories;
        }

        public static IList<Product> GetAllProducts()
        {
            var products = new List<Product>();

            foreach (var category in _categories)
            {
                products.AddRange(category.Products);
            }

            return products;
        }

        public static Product GetProductById(Guid id)
        {
            var products = GetAllProducts();
            return products.SingleOrDefault(p => p.Id == id);
        }

        public static IList<Product> GetProductsByCategoryId(Guid categoryId)
        {
            var category = GetCategoryById(categoryId);
            return category.Products;
        }

        public static Category GetCategoryById(Guid id)
        {
            return _categories.SingleOrDefault(c => c.Id == id);
        }

        public static void CreateCategory(Category category)
        {
            _categories.Add(category);
        }

        public static void DeleteCategory(Guid id)
        {
            _categories.Remove(_categories.Single(c => c.Id == id));
        }

        private static Guid GetRandomManufacturerId()
        {
            var random = new Random();
            var randomIndex = random.Next(0, _manufacturerIds.Length);
            return _manufacturerIds[randomIndex];
        }
    }
}
