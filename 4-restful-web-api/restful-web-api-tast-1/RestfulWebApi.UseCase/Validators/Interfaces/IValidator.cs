using RestfulWebApi.UseCase.DTOs;

namespace RestfulWebApi.UseCase.Validators.Interfaces
{
    public interface IValidator<T> where T : BaseDto
    {
        ValidationResult Validate(T item);
    }
}
