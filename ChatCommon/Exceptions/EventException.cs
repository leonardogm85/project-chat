namespace ChatCommon.Exceptions;

public sealed class EventException : BaseException
{
    public EventException()
    {
    }

    public EventException(string? message) : base(message)
    {
    }

    public EventException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
