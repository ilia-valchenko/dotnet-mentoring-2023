using System;
using AutoMapper;
using RestfulWebApi.Domain.ValueObjects;

namespace RestfulWebApi.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Infrastructure.Entities.Product, Domain.Entities.Product>()
                .ForMember(dest => dest.Id, src => src.MapFrom(p => new Guid(p.Id)))
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(p => new Guid(p.CategoryId)))
                .ForMember(dest => dest.ImageUrl, src => src.MapFrom(p => string.IsNullOrWhiteSpace(p.ImageUrl) ? null : new Url(p.ImageUrl)));

            CreateMap<Infrastructure.Entities.Category, Domain.Entities.Category>()
                .ForMember(dest => dest.Id, src => src.MapFrom(c => new Guid(c.Id)))
                .ForMember(dest => dest.ParentCategoryId, src => src.MapFrom(c => c.ParentCategoryId == null ? null : (Guid?)Guid.Parse(c.ParentCategoryId)))
                .ForMember(dest => dest.ImageUrl, src => src.MapFrom(p => string.IsNullOrWhiteSpace(p.ImageUrl) ? null : new Url(p.ImageUrl)));
        }
    }
}
