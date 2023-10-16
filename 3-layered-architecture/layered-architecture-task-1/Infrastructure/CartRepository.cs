using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Entities;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class CartRepository : ICartRepository
{
    private readonly string connectionString;
    private readonly ILogger<CartRepository> logger;

    public CartRepository(string connectionString, ILogger<CartRepository> logger)
    {
        this.connectionString = connectionString;
        this.logger = logger;
    }

    public async Task CreateAsync(CartDto cart, CancellationToken cancellationToken = default)
    {
        using var db = new LiteDatabase(this.connectionString);
        // No async version.
        var collection = db.GetCollection<CartDto>("carts");
        collection.Insert(cart);
        db.Commit();
    }

    public async Task<IList<CartDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        using var db = new LiteDatabase(this.connectionString);
        // No async version.
        var collection = db.GetCollection<CartDto>("carts");
        return collection.Query().ToList();
    }

    public async Task AddAsync(Guid cartId, CartItemDto itemToAdd, CancellationToken cancellationToken = default)
    {
        using var db = new LiteDatabase(this.connectionString);
        // No async version.
        var collection = db.GetCollection<CartDto>("carts");
        var cart = collection.FindOne(c => c.Id.Equals(cartId));
        cart.Items.Add(itemToAdd);
        collection.Update(cart);
        db.Commit();
    }

    public async Task RemoveAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default)
    {
        using var db = new LiteDatabase(this.connectionString);
        // No async version.
        var collection = db.GetCollection<CartDto>("carts");
        var cart = collection.FindOne(c => c.Id.Equals(cartId));
        cart.Items.Remove(cart.Items.Single(i => i.Id.Equals(cartItemId)));
        collection.Update(cart);
        db.Commit();
    }
}
