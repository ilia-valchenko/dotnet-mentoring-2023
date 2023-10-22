using AutoMapper;
using LayeredArchitecture.Domain.ValueObjects;
using LayeredArchitecture.UseCase.DTOs;

namespace LayeredArchitecture.UseCase
{
    public class MapperConfigurationFactory : IMapperConfigurationFactory
    {
        public MapperConfiguration CreateMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.Product, Product>()
                    .ForMember(dest => dest.ImageUrlText, opt => opt.MapFrom(src => src.ImageUrl == null ? null : src.ImageUrl.UrlText));

                cfg.CreateMap<Product, Domain.Entities.Product>()
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => new Url(src.ImageUrlText)));

                cfg.CreateMap<Domain.Entities.Category, Category>()
                    .ForMember(dest => dest.ImageUrlText, opt => opt.MapFrom(src => src.ImageUrl == null ? null : src.ImageUrl.UrlText));

                cfg.CreateMap<Category, Domain.Entities.Category>()
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => new Url(src.ImageUrlText)));
            });
        }
    }
}
