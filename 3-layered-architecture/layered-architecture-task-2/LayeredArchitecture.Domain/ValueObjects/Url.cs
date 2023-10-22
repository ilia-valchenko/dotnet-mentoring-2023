using System;
using LayeredArchitecture.Domain.Exceptions;

namespace LayeredArchitecture.Domain.ValueObjects
{
    public class Url
    {
        private string _urlText = string.Empty;

        public Url(string urlText)
        {
            this.UrlText = urlText;
        }

        public Url(string urlText, string altText)
        {
            this.UrlText = urlText;
            this.AltText = altText;
        }

        public string AltText { get; set; }

        public string UrlText
        {
            get
            {
                return _urlText;
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
                    throw new InvalidUrlFormatException("The provided string value is not a valid URL string.");
                }

                _urlText = value;
            }
        }
    }
}
