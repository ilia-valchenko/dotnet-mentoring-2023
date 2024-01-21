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
            Id = new Guid("E7EC3C0B-74AE-420E-A1A2-BE8C3B8064B8"),
            Name = "First item",
            Price = 10
        },
        new Item
        {
            Id = new Guid("926B5700-F305-48BC-B315-84596C77DCE1"),
            Name = "Second item",
            Price = 20
        },
        new Item
        {
            Id = new Guid("2654A315-FABD-47A4-B86F-C958DD1F49CB"),
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
