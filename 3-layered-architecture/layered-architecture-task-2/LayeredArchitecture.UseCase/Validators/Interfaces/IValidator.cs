using LayeredArchitecture.UseCase.DTOs;

namespace LayeredArchitecture.UseCase.Validators.Interfaces
{
    public interface IValidator<T> where T : BaseDto
    {
        ValidationResult Validate(T item);
    }
}
