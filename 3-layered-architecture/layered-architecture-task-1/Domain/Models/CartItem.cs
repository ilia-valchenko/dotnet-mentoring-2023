using Domain.ValueObjects;

namespace Domain.Models;

public class CartItem
{
    private readonly Guid _id;
    private readonly string _name;

    public CartItem(Guid id, string name)
    {
        if (id.Equals(Guid.Empty))
        {
            throw new ArgumentException("No empty GUIDs are allowed.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"The '{nameof(name)}' can't be null or a white-space.");
        }

        _id = id;
        _name = name;
    }

    public Guid Id => _id;
    public string Name => _name;
    public string? Description { get; set; }
    public Image? Image { get; set; }
    public decimal? Price { get; set; }
}