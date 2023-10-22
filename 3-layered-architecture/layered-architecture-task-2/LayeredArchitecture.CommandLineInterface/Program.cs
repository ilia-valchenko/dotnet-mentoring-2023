using System;
using LayeredArchitecture.UseCase.DTOs;
using LayeredArchitecture.UseCase.Services.Interfaces;

namespace LayeredArchitecture.CommandLineInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------- START: Task 2 --------------------");

            ICategoryServiceFactory categoryServiceFactory = new CategoryServiceFactory();
            IService<Category> categoryService = categoryServiceFactory.CreateCategoryService();

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "TestCategory1",
                ImageUrlText = "https://woocommerce.com/wp-content/uploads/2013/05/productcategory2.png"
            };

            try
            {
                categoryService.CreateAsync(category);

                Console.WriteLine("The new category has been successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong during creating a new category. Exception: {ex.ToString()}");
            }

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}
