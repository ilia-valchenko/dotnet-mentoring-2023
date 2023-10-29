using AutoMapper;
using RestfulWebApi.Domain.ValueObjects;
using RestfulWebApi.UseCase.DTOs;

namespace RestfulWebApi.UseCase
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Entities.Product, Product>()
                .ForMember(dest => dest.ImageUrlText, opt => opt.MapFrom(src => src.ImageUrl == null ? null : src.ImageUrl.UrlText));

            CreateMap<Product, Domain.Entities.Product>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrlText == null ? null : new Url(src.ImageUrlText)));

            CreateMap<Domain.Entities.Category, Category>()
                .ForMember(dest => dest.ImageUrlText, opt => opt.MapFrom(src => src.ImageUrl == null ? null : src.ImageUrl.UrlText));

            CreateMap<Category, Domain.Entities.Category>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrlText == null ? null : new Url(src.ImageUrlText)));
        }
    }
}
