namespace ChatServer.View;

internal abstract class BaseApplication
{
    protected static void WriteInfo(string message)
    {
        ForegroundColor(ConsoleColor.DarkGray);
        WriteLine(message);
    }

    protected static void WriteWarning(string message)
    {
        ForegroundColor(ConsoleColor.DarkYellow);
        WriteLine(message);
    }

    protected static void WriteError(string message)
    {
        ForegroundColor(ConsoleColor.DarkRed);
        WriteLine(message);
    }

    protected static string? ReadMessage()
    {
        ForegroundColor(ConsoleColor.DarkBlue);
        return ReadLine();
    }

    private static string? ReadLine()
    {
        return Console.ReadLine();
    }

    private static void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    private static void ForegroundColor(ConsoleColor color)
    {
        Console.ForegroundColor = color;
    }
}
