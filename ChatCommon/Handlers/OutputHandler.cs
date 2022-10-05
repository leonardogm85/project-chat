using ChatCommon.Models;

namespace ChatCommon.Handlers;

public sealed class OutputHandler
{
    private readonly StreamWriter _output;

    public OutputHandler(Stream stream)
    {
        _output = new(stream)
        {
            AutoFlush = true
        };
    }

    public void SendEnterRoomCommand(string name)
    {
        var command = new Command(CommandType.EnterRoom, name);
        _output.WriteLine(command);
    }

    public void SendEnterRoomResponseCommand(string error)
    {
        var command = new Command(CommandType.EnterRoomResponse, error);
        _output.WriteLine(command);
    }

    public void SendGetMembersCommand()
    {
        var command = new Command(CommandType.GetMembers, string.Empty);
        _output.WriteLine(command);
    }

    public void SendGetMembersResponseCommand(List<string> members)
    {
        var parameter = string
            .Join(",", members);

        var command = new Command(CommandType.GetMembersResponse, parameter);
        _output.WriteLine(command);
    }

    public void SendMessageCommand(string message)
    {
        var command = new Command(CommandType.SendMessage, message);
        _output.WriteLine(command);
    }

    public void SendMessageReceivedCommand(string message)
    {
        var command = new Command(CommandType.MessageReceived, message);
        _output.WriteLine(command);
    }

    public void SendMemberEnteredCommand(string name)
    {
        var command = new Command(CommandType.MemberEntered, name);
        _output.WriteLine(command);
    }

    public void SendMemberLeavingCommand(string name)
    {
        var command = new Command(CommandType.MemberLeaving, name);
        _output.WriteLine(command);
    }

    public void SendMemberCanLeaveCommand(string name)
    {
        var command = new Command(CommandType.MemberCanLeave, name);
        _output.WriteLine(command);
    }

    public void SendMemberLeftCommand(string name)
    {
        var command = new Command(CommandType.MemberLeft, name);
        _output.WriteLine(command);
    }

    public void SendServerConnectingCommand()
    {
        var command = new Command(CommandType.ServerConnecting, string.Empty);
        _output.WriteLine(command);
    }

    public void SendServerDisconnectingCommand()
    {
        var command = new Command(CommandType.ServerDisconnecting, string.Empty);
        _output.WriteLine(command);
    }
}
