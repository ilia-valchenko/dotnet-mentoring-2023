using RestfulWebApi.Application.DTOs;

namespace RestfulWebApi.Application;

public class TestInMemoryRepository : IRepository
{
    // Note: It's a mock which I use as a in-memory storage.
    private readonly IList<Cart> _carts = new List<Cart>();

    public async Task<IList<Cart>> GetAllCartsAsync(CancellationToken cancellationToken = default)
    {
        // That's ok. I use fake in-memory storage which doesn't have async methods.
        return await Task.FromResult(_carts);
    }

    public async Task<Cart?> GetCartByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // That's ok. I use fake in-memory storage which doesn't have async methods.
        return await Task.FromResult(_carts.FirstOrDefault(c => c.Id == id));
    }

    public async Task<Cart> CreateCartAsync(Cart cartToCreate, CancellationToken cancellationToken = default)
    {
        _carts.Add(cartToCreate);
        // That's ok. I use fake in-memory storage which doesn't have async methods.
        return await Task.FromResult(cartToCreate);
    }

    public async Task<IList<CartItem>> GetAllCartsItemsAsync(CancellationToken cancellationToken = default)
    {
        var items = new List<CartItem>();

        foreach (var cart in _carts)
        {
            if (cart != null && cart.Items != null && cart.Items.Any())
            {
                items.AddRange(cart.Items);
            }
        }

        // That's ok. I use fake in-memory storage which doesn't have async methods.
        return await Task.FromResult(items);
    }

    public async Task<CartItem?> GetCartItemByIdAsync(Guid cartId, Guid id, CancellationToken cancellationToken = default)
    {
        var items = await GetAllCartsItemsAsync(cancellationToken);
        return items.FirstOrDefault(i => i.CartId == cartId && i.Id == id);
    }

    public async Task<CartItem> CreateCartItemAsync(Guid cartId, CartItem cartItemToCreate, CancellationToken cancellationToken = default)
    {
        var cart = await GetCartByIdAsync(cartId, cancellationToken);
        cart.Items.Add(cartItemToCreate);
        return cartItemToCreate;
    }

    public async Task DeleteCartItemAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default)
    {
        var cart = await GetCartByIdAsync(cartId, cancellationToken);
        var cartItemToDelete = cart.Items.FirstOrDefault(i => i.Id == cartItemId);
        cart.Items.Remove(cartItemToDelete);
;    }
}
