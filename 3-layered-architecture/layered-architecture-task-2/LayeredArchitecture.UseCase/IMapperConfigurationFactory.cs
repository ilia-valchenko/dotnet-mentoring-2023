using AutoMapper;

namespace LayeredArchitecture.UseCase
{
    public interface IMapperConfigurationFactory
    {
        MapperConfiguration CreateMapperConfiguration();
    }
}
