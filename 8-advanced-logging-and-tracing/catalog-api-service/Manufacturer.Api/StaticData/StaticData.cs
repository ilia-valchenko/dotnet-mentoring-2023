namespace Manufacturer.Api.StaticData;

public class StaticData
{
    private static List<Models.Manufacturer> _manufacturers = new List<Models.Manufacturer>
    {
        new Models.Manufacturer
        {
            Id = Guid.Parse("ba212e9c-8201-4725-8198-b26e0738ff6e"),
            Name = "Nestle",
            EstablishDate = new DateTime(1866,1,1),
            WikiLink = "https://en.wikipedia.org/wiki/Nestl%C3%A9"
        },
        new Models.Manufacturer
        {
            Id = Guid.Parse("619ee7e7-7869-4016-883e-26f5cc8549db"),
            Name = "Unilever",
            EstablishDate = new DateTime(1929,1,1),
            WikiLink = "https://en.wikipedia.org/wiki/Unilever"
        },
        new Models.Manufacturer
        {
            Id = Guid.Parse("0f4b9300-8ba8-4529-b0fb-7c19451c913c"),
            Name = "JBS",
            EstablishDate = new DateTime(1855,1,1),
            WikiLink = "https://en.wikipedia.org/wiki/JBS_USA"
        }
    };

    public static IList<Models.Manufacturer> GetAllManufacturer()
    {
        return _manufacturers;
    }

    public static Models.Manufacturer GetManufacturerById(Guid id)
    {
        return _manufacturers.SingleOrDefault(m => m.Id == id);
    }
}