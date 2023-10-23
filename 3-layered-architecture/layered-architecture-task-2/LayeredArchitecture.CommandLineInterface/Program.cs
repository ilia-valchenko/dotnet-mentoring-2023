using System;
using System.Threading.Tasks;
using LayeredArchitecture.UseCase.DTOs;
using LayeredArchitecture.UseCase.Services.Interfaces;

namespace LayeredArchitecture.CommandLineInterface
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("-------------------- START: Task 2 --------------------");

            ICategoryServiceFactory categoryServiceFactory = new CategoryServiceFactory();
            IService<Category> categoryService = categoryServiceFactory.CreateCategoryService();
            var category = CreateCategory();

            try
            {
                await categoryService.CreateAsync(category);
                Console.WriteLine("The new category has been successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong during creating a new category. Exception: {ex.ToString()}");
            }

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }

        private static Category CreateCategory()
        {
            var categoryId = Guid.NewGuid();

            return new Category
            {
                Id = categoryId,
                Name = "TestCategory1",
                ImageUrlText = "https://woocommerce.com/wp-content/uploads/2013/05/productcategory2.png",
                Products = new[]
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        CategoryId = categoryId,
                        Name = "TestProduct1",
                        Description = "TestProduct1Description",
                        Price = 12,
                        Amount = 1
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        CategoryId = categoryId,
                        Name = "TestProduct1",
                        ImageUrlText = "https://greendroprecycling.com/wp-content/uploads/2017/04/GreenDrop_Station_Aluminum_Can_Coke.jpg",
                        Price = 2,
                        Amount = 1
                    }
                }
            };
        }
    }
}
