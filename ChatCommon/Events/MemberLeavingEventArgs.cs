namespace ChatCommon.Events;

public sealed class MemberLeavingEventArgs : CommandEventArgs
{
    public string Name { get; }

    public MemberLeavingEventArgs(string name)
    {
        Name = name;
    }
}
