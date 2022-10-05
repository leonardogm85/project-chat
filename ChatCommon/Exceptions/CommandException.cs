namespace ChatCommon.Exceptions;

public sealed class CommandException : BaseException
{
    public CommandException()
    {
    }

    public CommandException(string? message) : base(message)
    {
    }

    public CommandException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
