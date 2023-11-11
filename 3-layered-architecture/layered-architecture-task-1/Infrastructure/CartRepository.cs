using Application;
using Application.DTOs;
using Dapper;
using Domain.Models;
using LiteDB;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

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
        //await Task.Run(() =>
        //{
        //    using var db = new LiteDatabase(_connectionString);
        //    // No async version.
        //    var collection = db.GetCollection<Cart>("carts");
        //    collection.Insert(cart);
        //    db.Commit();
        //});

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        int numberOfAffectedRows = await connection.ExecuteAsync(
            "INSERT INTO Cart (Id, Quantity) VALUES (@Id, @Quantity);",
            new
            {
                Id = cart.Id.ToString(),
                Quantity = cart.Quantity
            });

        if (numberOfAffectedRows < 1)
        {
            throw new Exception("Failed to insert the new cart.");
        }
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

    public async Task<Cart> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            using var db = new LiteDatabase(_connectionString);
            // No async version.
            var collection = db.GetCollection<Cart>("carts");
            return collection.FindOne(c => c.Id == id);
        });
    }

    public async Task UpdateAsync(Cart cartToUpdate, CancellationToken cancellationToken = default)
    {
        //await Task.Run(() =>
        //{
        //    using var db = new LiteDatabase(_connectionString);
        //    // No async version.
        //    var collection = db.GetCollection<Cart>("carts");
        //    collection.Update(cartToUpdate);
        //    db.Commit();
        //});

        using var connection = new SqliteConnection(_connectionString);

        var numberOfAffectedRows = await connection.ExecuteAsync(
            "UPDATE Cart SET Quantity = @Quantity WHERE Id = @Id;",
            new
            {
                Id = cartToUpdate.Id.ToString(),
                Quantity = cartToUpdate.Quantity
            });

        if (numberOfAffectedRows < 1)
        {
            throw new Exception(
                "Failed to update the cart. " +
                $"Serialized category: {JsonConvert.SerializeObject(cartToUpdate)}.");
        }
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
