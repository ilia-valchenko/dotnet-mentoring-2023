using RestfulWebApi.Application.DTOs;

namespace RestfulWebApi.Application.Services.Interfaces;

public interface IService<T> where T : BaseDto
{
    Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default);
}