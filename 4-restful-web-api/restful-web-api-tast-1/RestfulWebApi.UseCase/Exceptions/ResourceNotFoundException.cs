using System;

namespace RestfulWebApi.UseCase.Exceptions
{
    public class ResourceNotFoundException<T> : Exception
    {
        public Type ResourceType => typeof(T);

        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
