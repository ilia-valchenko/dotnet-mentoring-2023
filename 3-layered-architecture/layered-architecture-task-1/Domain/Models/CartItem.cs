using Domain.ValueObjects;

namespace Domain.Models;

public class CartItem : BaseDomainModel
{
    private string _name = "Unknown";

    public CartItem() : base()
    {
    }

    public CartItem(Guid id) : base(id)
    {
    }

    public string Name {
        get
        {
            return _name;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"The '{nameof(Name)}' can't be null or a white-space.");
            }

            _name = value;
        }
    }

    public string? Description { get; set; }

    //public Image? Image { get; set; }

    //public decimal? Price { get; set; }
}