using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Models;
using LiteDB;

namespace Infrastructure;

public class CartRepository : ICartRepository
{
    private readonly string _connectionString;

    public CartRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            using var db = new LiteDatabase(_connectionString);
            // No async version.
            var collection = db.GetCollection<Cart>("carts");
            collection.Insert(cart);
            db.Commit();
        });
    }

    public async Task<IList<Cart>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            using var db = new LiteDatabase(_connectionString);
            // No async version.
            var collection = db.GetCollection<Cart>("carts");
            return collection.Query().ToList();
        });
    }

    public async Task AddItemAsync(Guid cartId, CartItem itemToAdd, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            using var db = new LiteDatabase(_connectionString);
            // No async version.
            var collection = db.GetCollection<Cart>("carts");
            var cart = collection.FindOne(c => c.Id.Equals(cartId));
            cart.Items.Add(itemToAdd);
            collection.Update(cart);
            db.Commit();
        });
    }

    public async Task RemoveItemAsync(Guid cartId, Guid cartItemId, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            using var db = new LiteDatabase(_connectionString);
            // No async version.
            var collection = db.GetCollection<CartDto>("carts");
            var cart = collection.FindOne(c => c.Id.Equals(cartId));
            cart.Items.Remove(cart.Items.Single(i => i.Id.Equals(cartItemId)));
            collection.Update(cart);
            db.Commit();
        });
    }
}
