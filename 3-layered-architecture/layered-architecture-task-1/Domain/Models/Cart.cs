namespace Domain.Models;

public class Cart : BaseDomainModel
{
    private readonly IList<CartItem> _items;

    public Cart() : this(Guid.NewGuid())
    {
    }

    public Cart(Guid id) : base(id)
    {
        _items = new List<CartItem>();
    }

    public IList<CartItem> Items => _items;

    public int Quantity { get; set; }
}