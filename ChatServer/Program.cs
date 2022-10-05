using ChatServer.ViewModels;

namespace ChatServer;

internal static class Program
{
    internal static async Task Main()
    {
        try
        {
            var viewModel = ServerViewModel.GetInstance();

            await viewModel.Connect();

            if (viewModel.Connected)
            {
                Console.WriteLine("Server connected. Press any key to disconnect.");
            }
            else
            {
                Console.WriteLine("Server disconnected. Press any key to close this window.");
            }

            Console.ReadLine();

            await viewModel.Disconnect();
        }
        catch (Exception)
        {
            Console.WriteLine("Error connecting to server. Press any key to close this window.");

            Console.ReadLine();
        }
    }
}
