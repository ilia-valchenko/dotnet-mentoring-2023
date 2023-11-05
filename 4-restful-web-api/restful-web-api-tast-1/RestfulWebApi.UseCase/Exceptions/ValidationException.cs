using System;

namespace RestfulWebApi.UseCase.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
