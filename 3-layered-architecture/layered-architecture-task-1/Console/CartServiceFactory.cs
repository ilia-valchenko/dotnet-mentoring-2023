using Application;
using Application.Common.Interfaces;
using Application.Services;
using AutoMapper;
using Infrastructure;

namespace Console;

public class CartServiceFactory : ICartServiceFactory
{
    public ICartService CreateCartService()
    {
        string connectionString = "CartData.db";
        IMapperConfigurationFactory mapperConfigurationFactory = new MapperConfigurationFactory();
        IMapperFactory mapperFactory = new MapperFactory(mapperConfigurationFactory);
        IMapper mapper = mapperFactory.CreateMapper();
        ICartRepository cartRepository = new CartRepository(connectionString);
        return new CartService(cartRepository, mapper);
    }
}