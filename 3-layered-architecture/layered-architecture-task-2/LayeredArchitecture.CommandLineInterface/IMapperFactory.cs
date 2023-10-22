using AutoMapper;

namespace LayeredArchitecture.CommandLineInterface
{
    public interface IMapperFactory
    {
        IMapper CreateMapper();
    }
}
