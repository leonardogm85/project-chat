namespace ChatCommon.Events;

public sealed class MessageReceivedEventArgs : CommandEventArgs
{
    public string Message { get; }

    public MessageReceivedEventArgs(string message)
    {
        Message = message;
    }
}
