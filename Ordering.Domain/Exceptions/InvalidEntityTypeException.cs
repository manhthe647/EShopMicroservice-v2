namespace Ordering.Domain.Exceptions;

public class InvalidEntityTypeException : ApplicationException
{
    public InvalidEntityTypeException(string entity, string type) :
        base(message: $"Entity \"{entity}\" not supported type: {type}")
    {
    }
}