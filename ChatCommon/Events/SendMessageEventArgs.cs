namespace ChatCommon.Events;

public sealed class SendMessageEventArgs : CommandEventArgs
{
    public string Message { get; }

    public SendMessageEventArgs(string message)
    {
        Message = message;
    }
}
