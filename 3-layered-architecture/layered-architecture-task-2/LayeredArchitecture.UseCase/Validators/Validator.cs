using System;
using LayeredArchitecture.Domain.Constants;
using LayeredArchitecture.Domain.Exceptions;
using LayeredArchitecture.UseCase.DTOs;
using LayeredArchitecture.UseCase.Validators.Interfaces;

namespace LayeredArchitecture.UseCase.Validators
{
    public class Validator : IValidator<BaseDto>
    {
        public ValidationResult Validate(BaseDto item)
        {
            if (item == null)
            {
                return new ValidationResult(new[] {
                    new ArgumentNullException(nameof(item))
                });
            }

            var validationResult = new ValidationResult();

            if (item.Id.Equals(Guid.Empty))
            {
                validationResult.Exceptions.Add(new ArgumentException("No empty GUIDs are allowed."));
            }

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                validationResult.Exceptions.Add(new InvalidNameException($"The length of the provided name is greater than {Limits.NameLengthLimit} characters."));
            }

            if (item.ImageUrlText != null && !(Uri.TryCreate(item.ImageUrlText, UriKind.Absolute, out var uriResult)
                    && uriResult.Scheme == Uri.UriSchemeHttp
                    || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                validationResult.Exceptions.Add(new InvalidUrlFormatException("The provided string value is not a valid URL string."));
            }

            return validationResult;
        }
    }
}
