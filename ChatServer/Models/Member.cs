using System.Net.Sockets;
using ChatCommon.Events;
using ChatCommon.Handlers;

namespace ChatServer.Models;

internal sealed class Member
{
    private readonly Server _server;

    public InputHandler InputHandler { get; }
    public OutputHandler OutputHandler { get; }

    public string? Name { get; private set; }

    public Member(Server server, TcpClient tcpClient)
    {
        _server = server;

        InputHandler = new InputHandler(tcpClient.GetStream());
        OutputHandler = new OutputHandler(tcpClient.GetStream());
    }

    public async Task HandleMemberInteraction()
    {
        InputHandler.EnterRoom += OnEnterRoom;
        InputHandler.GetMembers += OnGetMembers;
        InputHandler.SendMessage += OnSendMessage;
        InputHandler.MemberLeaving += OnMemberLeaving;

        await InputHandler.Start();
    }

    public void SendServerDisconnectingCommand()
    {
        OutputHandler.SendServerDisconnectingCommand();
    }

    private void OnEnterRoom(object? sender, EnterRoomEventArgs e)
    {
        Name = e.Name;

        var error = string.Empty;

        lock (_server.Members)
        {
            foreach (var member in _server.Members)
            {
                if (member == this)
                {
                    continue;
                }

                if (member.Name == e.Name)
                {
                    error = $"The name {Name} already exists in chat.";

                    break;
                }
            }
        }

        OutputHandler.SendEnterRoomResponseCommand(error);

        if (string.IsNullOrEmpty(error))
        {
            lock (_server.Members)
            {
                foreach (var member in _server.Members)
                {
                    member.OutputHandler.SendMemberEnteredCommand(Name!);
                }

                _server.Members.Add(this);
            }
        }
        else
        {
            e.InputHandlerStopped = true;
        }
    }

    private void OnGetMembers(object? sender, GetMembersEventArgs e)
    {
        lock (_server.Members)
        {
            _server.Members.Remove(this);

            foreach (var member in _server.Members)
            {
                member.OutputHandler.SendMemberLeftCommand(Name!);
            }
        }

        OutputHandler.SendMemberCanLeaveCommand(Name!);

        e.InputHandlerStopped = true;
    }

    private void OnSendMessage(object? sender, SendMessageEventArgs e)
    {
        var message = $"[{DateTime.Now} {Name} - {e.Message}]";

        lock (_server.Members)
        {
            foreach (var member in _server.Members)
            {
                member.OutputHandler.SendMessageReceivedCommand(message);
            }
        }
    }

    private void OnMemberLeaving(object? sender, MemberLeavingEventArgs e)
    {
        var members = new List<string>();

        lock (_server.Members)
        {
            members = _server.Members
                .Select(member => member.Name!)
                .ToList();
        }

        OutputHandler.SendGetMembersResponseCommand(members);
    }
}
