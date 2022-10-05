using System.Net;
using System.Net.Sockets;

namespace ChatServer.Models;

internal sealed class Server
{
    public event EventHandler? Connected;
    public event EventHandler? Disconnected;

    private TcpListener? _tcpListener;

    private bool _running = false;

    private readonly object _syncRunning = new();

    public List<Member> Members { get; }

    public Server()
    {
        Members = new();
    }

    public async Task Connect(int port)
    {
        await Task.Factory.StartNew(async () => await HandleConnection(port));
    }

    public async Task Disconnect()
    {
        lock (Members)
        {
            foreach (var member in Members)
            {
                member.SendServerDisconnectingCommand();
            }
        }

        while (true)
        {
            lock (Members)
            {
                if (Members.Count == 0)
                {
                    break;
                }
            }

            await Task.Delay(1000);
        }

        lock (_syncRunning)
        {
            _running = false;
        }
    }

    private async Task HandleConnection(int port)
    {
        _tcpListener = new(IPAddress.Loopback, port);

        _tcpListener.Start();

        lock (_syncRunning)
        {
            _running = true;
        }

        Connected?.Invoke(this, EventArgs.Empty);

        while (true)
        {
            if (_tcpListener.Pending())
            {
                var tcpClient = _tcpListener.AcceptTcpClient();
                var member = new Member(this, tcpClient);

                await member.HandleMemberInteraction();
            }
            else
            {
                lock (_syncRunning)
                {
                    if (!_running)
                    {
                        break;
                    }
                }

                await Task.Delay(1000);
            }
        }

        _tcpListener.Stop();

        Disconnected?.Invoke(this, EventArgs.Empty);
    }
}
