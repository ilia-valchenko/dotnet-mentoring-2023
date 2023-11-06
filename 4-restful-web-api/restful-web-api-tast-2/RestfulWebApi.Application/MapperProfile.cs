using AutoMapper;
using RestfulWebApi.Application.DTOs;

namespace RestfulWebApi.Application;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateCart, Cart>();
        CreateMap<CreateCartItem, CartItem>();
    }
}