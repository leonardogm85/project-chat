namespace ChatCommon.Events;

public sealed class GetMembersResponseEventArgs : CommandEventArgs
{
    public List<string> Members { get; }

    public GetMembersResponseEventArgs(List<string> members)
    {
        Members = members;
    }
}
