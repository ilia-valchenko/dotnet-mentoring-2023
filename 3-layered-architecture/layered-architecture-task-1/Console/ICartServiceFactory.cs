using Application.Common.Interfaces;

namespace Console;

public interface ICartServiceFactory
{
    ICartService CreateCartService();
}