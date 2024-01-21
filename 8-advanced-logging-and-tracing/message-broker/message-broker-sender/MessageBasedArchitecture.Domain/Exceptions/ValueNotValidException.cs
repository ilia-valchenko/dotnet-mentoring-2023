namespace MessageBasedArchitecture.Domain.Exceptions;

public class ValueNotValidException : Exception
{
    public ValueNotValidException() : base()
    {
    }

    public ValueNotValidException(string errorMessage) : base(errorMessage)
    {
    }
}