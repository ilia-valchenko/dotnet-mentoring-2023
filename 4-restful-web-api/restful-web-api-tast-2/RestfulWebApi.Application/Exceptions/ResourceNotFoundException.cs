using RestfulWebApi.Application.DTOs;

namespace RestfulWebApi.Application.Exceptions
{
    internal class ResourceNotFoundException<T> : Exception where T : BaseDto
    {
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
