namespace Domain.ValueObjects;

public class Image
{
    private string _url = string.Empty;

    public string? AltText { get; set; }

    public string Url {
        get
        {
            return _url;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The provided string value is null or a white-space.");
            }

            if (!(Uri.TryCreate(value, UriKind.Absolute, out var uriResult)
                && uriResult.Scheme == Uri.UriSchemeHttp
                || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                throw new ArgumentException("The provided string value is not a valid URL string.");
            }

            _url = value;
        }
    }
}
