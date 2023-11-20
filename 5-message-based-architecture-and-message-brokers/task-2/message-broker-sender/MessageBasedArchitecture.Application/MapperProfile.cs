using AutoMapper;

namespace MessageBasedArchitecture.Application;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Domain.Entities.Item, Application.Models.Item>();
    }
}