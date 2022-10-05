namespace ChatCommon.Events;

public sealed class EnterRoomEventArgs : CommandEventArgs
{
    public string Name { get; }

    public EnterRoomEventArgs(string name)
    {
        Name = name;
    }
}
