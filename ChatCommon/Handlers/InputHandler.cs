using ChatCommon.Events;
using ChatCommon.Exceptions;
using ChatCommon.Models;

namespace ChatCommon.Handlers;

public sealed class InputHandler
{
    public event EventHandler<EnterRoomEventArgs>? EnterRoom;
    public event EventHandler<EnterRoomResponseEventArgs>? EnterRoomResponse;

    public event EventHandler<GetMembersEventArgs>? GetMembers;
    public event EventHandler<GetMembersResponseEventArgs>? GetMembersResponse;

    public event EventHandler<SendMessageEventArgs>? SendMessage;

    public event EventHandler<MessageReceivedEventArgs>? MessageReceived;

    public event EventHandler<MemberEnteredEventArgs>? MemberEntered;
    public event EventHandler<MemberLeavingEventArgs>? MemberLeaving;
    public event EventHandler<MemberCanLeaveEventArgs>? MemberCanLeave;
    public event EventHandler<MemberLeftEventArgs>? MemberLeft;

    public event EventHandler<ServerConnectingEventArgs>? ServerConnecting;
    public event EventHandler<ServerDisconnectingEventArgs>? ServerDisconnecting;

    private bool _stopped = false;

    private readonly StreamReader _input;

    public InputHandler(Stream stream)
    {
        _input = new(stream);
    }

    public async Task Start()
    {
        await Task.Factory.StartNew(Run);
    }

    private void Run()
    {
        while (!_stopped)
        {
            var commandText = _input.ReadLine();

            if (commandText == null)
            {
                throw new CommandException("The command provided is null.");
            }

            var command = Command.Parse(commandText);

            switch (command.Type)
            {
                case CommandType.EnterRoom:
                    HandleEnterRoomCommand(command);
                    break;
                case CommandType.EnterRoomResponse:
                    HandleEnterRoomResponseCommand(command);
                    break;
                case CommandType.GetMembers:
                    HandleGetMembersCommand(command);
                    break;
                case CommandType.GetMembersResponse:
                    HandleGetMembersResponseCommand(command);
                    break;
                case CommandType.SendMessage:
                    HandleSendMessageCommand(command);
                    break;
                case CommandType.MessageReceived:
                    HandleMessageReceivedCommand(command);
                    break;
                case CommandType.MemberEntered:
                    HandleMemberEnteredCommand(command);
                    break;
                case CommandType.MemberLeaving:
                    HandleMemberLeavingCommand(command);
                    break;
                case CommandType.MemberCanLeave:
                    HandleMemberCanLeaveCommand(command);
                    break;
                case CommandType.MemberLeft:
                    HandleMemberLeftCommand(command);
                    break;
                case CommandType.ServerConnecting:
                    HandleServerConnectingCommand(command);
                    break;
                case CommandType.ServerDisconnecting:
                    HandleServerDisconnectingCommand(command);
                    break;
                default:
                    throw new CommandException("The given command type was not found.");
            }
        }
    }

    public void HandleEnterRoomCommand(Command command)
    {
        if (EnterRoom == null)
        {
            return;
        }

        var args = new EnterRoomEventArgs(command.Parameter);
        EnterRoom.Invoke(this, args);
        _stopped = args.InputHandlerStopped;
    }

    public void HandleEnterRoomResponseCommand(Command command)
    {
        if (EnterRoomResponse == null)
        {
            return;
        }

        var args = new EnterRoomResponseEventArgs(command.Parameter);
        EnterRoomResponse.Invoke(this, args);
        _stopped = args.InputHandlerStopped;
    }

    public void HandleGetMembersCommand(Command command)
    {
        if (GetMembers == null)
        {
            return;
        }

        var args = new GetMembersEventArgs();
        GetMembers.Invoke(this, args);
        _stopped = args.InputHandlerStopped;
    }

    public void HandleGetMembersResponseCommand(Command command)
    {
        if (GetMembersResponse == null)
        {
            return;
        }

        var members = command.Parameter
            .Split(",")
            .ToList();

        var args = new GetMembersResponseEventArgs(members);
        GetMembersResponse.Invoke(this, args);
        _stopped = args.InputHandlerStopped;
    }

    public void HandleSendMessageCommand(Command command)
    {
        if (SendMessage == null)
        {
            return;
        }

        var args = new SendMessageEventArgs(command.Parameter);
        SendMessage.Invoke(this, args);
        _stopped = args.InputHandlerStopped;
    }

    public void HandleMessageReceivedCommand(Command command)
    {
        if (MessageReceived == null)
        {
            return;
        }

        var args = new MessageReceivedEventArgs(command.Parameter);
        MessageReceived.Invoke(this, args);
        _stopped = args.InputHandlerStopped;
    }

    public void HandleMemberEnteredCommand(Command command)
    {
        if (MemberEntered == null)
        {
            return;
        }

        var args = new MemberEnteredEventArgs(command.Parameter);
        MemberEntered.Invoke(this, args);
        _stopped = args.InputHandlerStopped;
    }

    public void HandleMemberLeavingCommand(Command command)
    {
        if (MemberLeaving == null)
        {
            return;
        }

        var args = new MemberLeavingEventArgs(command.Parameter);
        MemberLeaving.Invoke(this, args);
        _stopped = args.InputHandlerStopped;
    }

    public void HandleMemberCanLeaveCommand(Command command)
    {
        if (MemberCanLeave == null)
        {
            return;
        }

        var args = new MemberCanLeaveEventArgs(command.Parameter);
        MemberCanLeave.Invoke(this, args);
        _stopped = args.InputHandlerStopped;
    }

    public void HandleMemberLeftCommand(Command command)
    {
        if (MemberLeft == null)
        {
            return;
        }

        var args = new MemberLeftEventArgs(command.Parameter);
        MemberLeft.Invoke(this, args);
        _stopped = args.InputHandlerStopped;
    }

    public void HandleServerConnectingCommand(Command command)
    {
        if (ServerConnecting == null)
        {
            return;
        }

        var args = new ServerConnectingEventArgs();
        ServerConnecting.Invoke(this, args);
        _stopped = args.InputHandlerStopped;
    }

    public void HandleServerDisconnectingCommand(Command command)
    {
        if (ServerDisconnecting == null)
        {
            return;
        }

        var args = new ServerDisconnectingEventArgs();
        ServerDisconnecting.Invoke(this, args);
        _stopped = args.InputHandlerStopped;
    }
}
