using LayeredArchitecture.UseCase.DTOs;
using LayeredArchitecture.UseCase.Services.Interfaces;

namespace LayeredArchitecture.CommandLineInterface
{
    public interface ICategoryServiceFactory
    {
        IService<Category> CreateCategoryService();
    }
}
