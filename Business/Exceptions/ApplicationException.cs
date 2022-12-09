using System.Runtime.Serialization;

namespace Business.Exceptions;

/// <summary>
/// Represents the general application error
/// </summary>
[Serializable]
public class ApplicationException : Exception
{
    public ApplicationException()
    {
    }

    protected ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ApplicationException(string? message) : base(message)
    {
    }

    public ApplicationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}