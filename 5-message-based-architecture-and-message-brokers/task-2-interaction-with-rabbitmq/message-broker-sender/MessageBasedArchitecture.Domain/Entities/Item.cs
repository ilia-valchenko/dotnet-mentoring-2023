using MessageBasedArchitecture.Domain.Exceptions;

namespace MessageBasedArchitecture.Domain.Entities;

public class Item : BaseItem
{
    private string _name = string.Empty;
    private double _price;

    public string Name {
        get { 
            return _name;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ValueNotValidException($"The '{nameof(Name)}' is null or a white-space.");
            }

            _name = value;
        }
    }

    public double Price {
        get
        {
            return _price;
        }
        set
        {
            if (value < 0)
            {
                throw new ValueNotValidException($"The '{nameof(Price)}' can't be negative.");
            }

            _price = value;
        }
    }
}