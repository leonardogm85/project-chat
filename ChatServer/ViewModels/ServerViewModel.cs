using ChatCommon.Bind;
using ChatServer.Models;

namespace ChatServer.ViewModels;

internal sealed class ServerViewModel : Bindable
{
    private static readonly Lazy<ServerViewModel> _instance = new(() => new());

    public const int MinimumPort = 1000;
    public const int MaximumPort = 9000;

    private readonly Server _server;

    public int Port { get; private set; } = 0;
    public bool Connected { get; private set; } = false;

    private ServerViewModel()
    {
        _server = new();

        _server.Connected += OnConnected;
        _server.Disconnected += OnDisconnected;
    }

    public static ServerViewModel GetInstance()
    {
        return _instance.Value;
    }

    public void SetPort(string? input)
    {
        if (int.TryParse(input, out var port))
        {
            if (port < MinimumPort)
            {
                AddError($"The port must be greater than or equal to {MinimumPort}.", nameof(Port));
            }
            else if (port > MaximumPort)
            {
                AddError($"The port must be less than or equal to {MaximumPort}.", nameof(Port));
            }
            else
            {
                RemoveErrors(nameof(Port));
            }
        }
        else
        {
            AddError("The port must be an integer.", nameof(Port));
        }

        Port = port;
    }

    public async Task Connect()
    {
        if (Connected)
        {
            return;
        }

        await _server.Connect(Port);
    }

    public async Task Disconnect()
    {
        if (!Connected)
        {
            return;
        }

        await _server.Disconnect();
    }

    private void OnConnected(object? sender, EventArgs e)
    {
        Connected = true;
    }

    private void OnDisconnected(object? sender, EventArgs e)
    {
        Connected = false;
    }
}
