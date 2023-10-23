using AutoMapper;
using LayeredArchitecture.Infrastructure;
using LayeredArchitecture.UseCase;
using LayeredArchitecture.UseCase.DTOs;
using LayeredArchitecture.UseCase.Services;
using LayeredArchitecture.UseCase.Services.Interfaces;
using LayeredArchitecture.UseCase.Validators;
using LayeredArchitecture.UseCase.Validators.Interfaces;

namespace LayeredArchitecture.CommandLineInterface
{
    public class CategoryServiceFactory : ICategoryServiceFactory
    {
        public IService<Category> CreateCategoryService()
        {
            string connectionString = "Data Source=../../../../AppData/catalog-service-database.db;";
            IMapperConfigurationFactory mapperConfigurationFactory = new MapperConfigurationFactory();
            IMapperFactory mapperFactory = new MapperFactory(mapperConfigurationFactory);
            IMapper mapper = mapperFactory.CreateMapper();
            IValidator<Category> validator = new Validator<Category>();
            IRepository<Domain.Entities.Category> repository = new CategoryRepository(connectionString);
            return new CategoryService(validator, repository, mapper);
        }
    }
}
