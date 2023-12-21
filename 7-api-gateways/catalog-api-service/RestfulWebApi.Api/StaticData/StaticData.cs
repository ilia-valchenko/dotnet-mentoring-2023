using System;
using System.Collections.Generic;
using System.Linq;
using RestfulWebApi.Api.Models;

namespace RestfulWebApi.Api.StaticData
{
    public static class StaticData
    {
        private const int NumberOfCategories = 5;
        private const int NumberOfProducts = 3;

        private static List<Category> _categories = new List<Category>();

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

        public static List<Category> GetAllCategories()
        {
            return _categories;
        }

        public static List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            foreach (var category in _categories)
            {
                products.AddRange(category.Products);
            }

            return products;
        }

        public static Category GetCategoryById(Guid id)
        {
            return _categories.SingleOrDefault(c => c.Id == id);
        }

        public static void CreateCategory(Category category)
        {
            _categories.Add(category);
        }
    }
}
