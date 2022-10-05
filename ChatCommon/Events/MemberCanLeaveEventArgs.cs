namespace ChatCommon.Events;

public sealed class MemberCanLeaveEventArgs : CommandEventArgs
{
    public string Name { get; }

    public MemberCanLeaveEventArgs(string name)
    {
        Name = name;
    }
}
