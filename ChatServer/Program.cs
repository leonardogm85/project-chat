using ChatServer.View;

namespace ChatServer;

internal sealed partial class Program
{
    internal static async Task Main()
    {
        await ServerApplication.GetInstance().Initialize();
    }
}
