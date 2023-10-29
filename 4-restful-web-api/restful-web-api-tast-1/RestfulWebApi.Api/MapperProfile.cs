using AutoMapper;
using RestfulWebApi.Api.ViewModels;

namespace RestfulWebApi.Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UseCase.DTOs.Category, Category>().ReverseMap();
            CreateMap<UseCase.DTOs.Product, Product>().ReverseMap();
        }
    }
}
