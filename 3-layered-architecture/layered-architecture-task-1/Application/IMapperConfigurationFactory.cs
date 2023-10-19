using AutoMapper;

namespace Application;

public interface IMapperConfigurationFactory
{
    MapperConfiguration CreateMapperConfiguration();
}