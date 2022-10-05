namespace ChatCommon.Models;

public enum CommandType
{
    EnterRoom,
    EnterRoomResponse,

    GetMembers,
    GetMembersResponse,

    SendMessage,

    MessageReceived,

    MemberEntered,
    MemberLeaving,
    MemberCanLeave,
    MemberLeft,

    ServerConnecting,
    ServerDisconnecting
}
