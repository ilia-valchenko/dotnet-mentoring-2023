namespace Domain.Models;

public class Cart
{
    private readonly Guid _id;
    private readonly IList<CartItem> _items;

    public Cart(Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            throw new ArgumentException("No empty GUIDs are allowed.");
        }

        _id = id;
        _items = new List<CartItem>();
    }

    public Guid Id => _id;
    public IList<CartItem> Items => _items;
    public int Quantity => _items.Count;
}