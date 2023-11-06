using RestfulWebApi.Application.DTOs;

namespace RestfulWebApi.Application.Services.Interfaces;

public interface IService<T> where T : BaseDto
{
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}