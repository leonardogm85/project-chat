namespace ChatCommon.Events;

public sealed class MemberLeftEventArgs : CommandEventArgs
{
    public string Name { get; }

    public MemberLeftEventArgs(string name)
    {
        Name = name;
    }
}
