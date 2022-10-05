namespace ChatCommon.Events;

public sealed class MemberEnteredEventArgs : CommandEventArgs
{
    public string Name { get; }

    public MemberEnteredEventArgs(string name)
    {
        Name = name;
    }
}
