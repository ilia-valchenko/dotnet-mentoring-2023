using MessageBasedArchitecture.Application;
using MessageBasedArchitecture.Domain.Entities;
using MessageBasedArchitecture.Domain.Exceptions;

namespace MessageBasedArchitecture.Infrastructure;

public class InMemoryRepository : IRepository
{
    private readonly List<Item> _items = new List<Item>
    {
        new Item
        {
            Id = Guid.NewGuid(),
            Name = "First item",
            Price = 10
        },
        new Item
        {
            Id = Guid.NewGuid(),
            Name = "Second item",
            Price = 20
        },
        new Item
        {
            Id = Guid.NewGuid(),
            Name = "Third item",
            Price = 30
        }
    };

    public async Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_items);
    }

    public async Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        return await Task.FromResult(item);
    }

    public async Task UpdateAsync(Item item, CancellationToken cancellationToken = default)
    {
        var existingItem = _items.FirstOrDefault(i => i.Id == item.Id);

        if (existingItem == null)
        {
            throw new ResourceNotFoundException();
        }

        existingItem.Name = item.Name;
        existingItem.Price = item.Price;

        await Task.Delay(1000);
    }
}
