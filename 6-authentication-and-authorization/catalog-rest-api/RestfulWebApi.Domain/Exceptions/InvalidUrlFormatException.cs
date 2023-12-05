using System;

namespace RestfulWebApi.Domain.Exceptions
{
    public class InvalidUrlFormatException : Exception
    {
        public InvalidUrlFormatException(string message) : base(message)
        {
        }
    }
}
