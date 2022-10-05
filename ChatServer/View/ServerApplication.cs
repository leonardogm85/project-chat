using ChatCommon.Events;
using ChatServer.ViewModels;

namespace ChatServer.View;

internal sealed class ServerApplication : BaseApplication
{
    private static readonly Lazy<ServerApplication> _instance = new(() => new());

    public ServerViewModel ViewModel { get; }

    private ServerApplication()
    {
        ViewModel = ServerViewModel.GetInstance();
        ViewModel.ErrorChanged += OnErrorChanged;
    }

    public static ServerApplication GetInstance() => _instance.Value;

    public async Task Initialize()
    {
        try
        {
            WriteInfo("Server initializing.");
            ProvidePort();

            await ViewModel.Connect();

            if (ViewModel.Connected)
            {
                WriteInfo("Server connected. Press any key to disconnect.");
            }
            else
            {
                WriteInfo("Server disconnected. Press any key to close this window.");
            }

            ReadMessage();
        }
        catch (Exception)
        {
            WriteError("Error connecting to server. Press any key to close this window.");
            ReadMessage();
        }
        finally
        {
            await ViewModel.Disconnect();
        }
    }

    public void ProvidePort()
    {
        while (true)
        {
            WriteWarning("Enter a valid port:");
            ViewModel.SetPort(ReadMessage());

            if (!ViewModel.HasErrors(nameof(ServerViewModel.Port)))
            {
                break;
            }
        }
    }

    private void OnErrorChanged(object? sender, ErrorChangedEventArgs e)
    {
        if (sender is not ServerViewModel viewModel)
        {
            return;
        }

        foreach (var error in viewModel.GetErrors(e.PropertyName))
        {
            WriteError(error);
        }
    }
}
