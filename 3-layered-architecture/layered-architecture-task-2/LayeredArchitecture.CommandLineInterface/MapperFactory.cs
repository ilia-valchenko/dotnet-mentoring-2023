using AutoMapper;
using LayeredArchitecture.UseCase;

namespace LayeredArchitecture.CommandLineInterface
{
    public class MapperFactory : IMapperFactory
    {
        private readonly IMapperConfigurationFactory _mapperConfigurationFactory;

        public MapperFactory(IMapperConfigurationFactory mapperConfigurationFactory)
        {
            _mapperConfigurationFactory = mapperConfigurationFactory;
        }

        public IMapper CreateMapper()
        {
            var configuration = _mapperConfigurationFactory.CreateMapperConfiguration();
            return configuration.CreateMapper();
        }
    }
}
