using AutoMapper;
using RestfulWebApi.Domain.ValueObjects;

namespace RestfulWebApi.UseCase
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Entities.Product, UseCase.DTOs.Product>()
                .ForMember(dest => dest.ImageUrlText, opt => opt.MapFrom(src => src.ImageUrl == null ? null : src.ImageUrl.UrlText));

            CreateMap<UseCase.DTOs.Product, Domain.Entities.Product>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrlText == null ? null : new Url(src.ImageUrlText)));

            CreateMap<UseCase.DTOs.CreateProduct, UseCase.DTOs.Product>();

            CreateMap<Domain.Entities.Category, UseCase.DTOs.Category>()
                .ForMember(dest => dest.ImageUrlText, opt => opt.MapFrom(src => src.ImageUrl == null ? null : src.ImageUrl.UrlText));

            CreateMap<UseCase.DTOs.Category, Domain.Entities.Category>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrlText == null ? null : new Url(src.ImageUrlText)));

            CreateMap<UseCase.DTOs.CreateCategory, UseCase.DTOs.Category>();
        }
    }
}
