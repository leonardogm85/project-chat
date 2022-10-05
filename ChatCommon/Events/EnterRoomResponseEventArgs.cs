namespace ChatCommon.Events;

public sealed class EnterRoomResponseEventArgs : CommandEventArgs
{
    public string Error { get; }

    public EnterRoomResponseEventArgs(string error)
    {
        Error = error;
    }

    public bool IsValid()
    {
        return string.IsNullOrEmpty(Error);
    }
}
