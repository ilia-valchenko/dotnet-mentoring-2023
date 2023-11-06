using AutoMapper;
using Microsoft.Extensions.Logging;
using RestfulWebApi.Application.DTOs;
using RestfulWebApi.Application.Services.Interfaces;

namespace RestfulWebApi.Application.Services;

public class CartService : ICartService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CartService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartService"/> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="logger">The logger.</param>
    public CartService(IRepository repository, IMapper mapper, ILogger<CartService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IList<Cart>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllCartsAsync(cancellationToken);
    }

    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.GetCartByIdAsync(id, cancellationToken);
    }

    public async Task<Cart> CreateAsync(CreateCart cartToCreate, CancellationToken cancellationToken = default)
    {
        // TODO: Validate the incoming cart model.
        var cart = _mapper.Map<Cart>(cartToCreate);
        cart.Id = Guid.NewGuid();

        foreach (var cartItem in cart.Items)
        {
            cartItem.Id = Guid.NewGuid();
        }

        var createdCart = await _repository.CreateCartAsync(cart, cancellationToken);
        return createdCart;
    }
}