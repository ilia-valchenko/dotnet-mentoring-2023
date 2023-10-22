using System;

namespace LayeredArchitecture.Domain.Exceptions
{
    public class InvalidUrlFormatException : Exception
    {
        public InvalidUrlFormatException(string message) : base(message)
        {
        }
    }
}
