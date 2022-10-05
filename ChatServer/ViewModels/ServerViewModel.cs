namespace ChatServer.ViewModels;

internal sealed class ServerViewModel
{
    private static readonly Lazy<ServerViewModel> _instance = new(() => new());

    private readonly Models.Server _server;

    private readonly int _port;

    public bool Connected { get; private set; } = false;

    private ServerViewModel()
    {
        _server = new();

        _port = 81;

        _server.Connected += (sender, e) => Connected = true;
        _server.Disconnected += (sender, e) => Connected = false;
    }

    public static ServerViewModel GetInstance()
    {
        return _instance.Value;
    }

    public async Task Connect()
    {
        await _server.Connect(_port);
    }

    public async Task Disconnect()
    {
        await _server.Disconnect();
    }
}
