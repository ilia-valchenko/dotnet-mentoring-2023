using AutoMapper;
using Microsoft.Extensions.Logging;
using RestfulWebApi.Application.DTOs;
using RestfulWebApi.Application.Exceptions;
using RestfulWebApi.Application.Services.Interfaces;

namespace RestfulWebApi.Application.Services;

public class CartItemService : ICartItemService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CartService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartItemService"/> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="logger">The logger.</param>
    public CartItemService(IRepository repository, IMapper mapper, ILogger<CartService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IList<CartItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllCartsItemsAsync(cancellationToken);
    }

    public async Task<CartItem?> GetByIdAsync(Guid cartId, Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.GetCartItemByIdAsync(cartId, id, cancellationToken);
    }

    public async Task<CartItem> CreateAsync(Guid cartId, CreateCartItem cartItemToCreate, CancellationToken cancellationToken = default)
    {
        var cart = await _repository.GetCartByIdAsync(cartId, cancellationToken);

        if (cart == null)
        {
            throw new ResourceNotFoundException<CartItem>();
        }

        var cartItem = _mapper.Map<CartItem>(cartItemToCreate);
        var createdCartItem = await _repository.CreateCartItemAsync(cartId, cartItem, cancellationToken);
        return createdCartItem;
    }

    public async Task DeleteAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default)
    {
        var cart = await _repository.GetCartByIdAsync(cartId, cancellationToken);

        if (cart == null)
        {
            throw new ResourceNotFoundException<Cart>();
        }

        var cartItemToDelete = cart.Items.FirstOrDefault(i => i.Id == cartItemId);

        if (cartItemToDelete == null)
        {
            throw new ResourceNotFoundException<CartItem>();
        }

        await _repository.DeleteCartItemAsync(cartId, cartItemId, cancellationToken);
    }
}