using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application;

public class MapperConfigurationFactory : IMapperConfigurationFactory
{
    public MapperConfiguration CreateMapperConfiguration()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Cart, CartDto>().ReverseMap();
            cfg.CreateMap<CartItem, CartItemDto>().ReverseMap();
        });
    }
}