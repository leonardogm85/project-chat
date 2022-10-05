using System.Text.RegularExpressions;
using ChatCommon.Exceptions;

namespace ChatCommon.Models;

public sealed class Command
{
    public CommandType Type { get; private set; }
    public string Parameter { get; private set; }

    public Command(CommandType type, string parameter)
    {
        Type = type;
        Parameter = parameter;
    }

    public static Command Parse(string input)
    {
        var pattern = @"^(\w+)\|(.*)$";

        if (Regex.IsMatch(input, pattern))
        {
            var typeText = Regex.Replace(input, pattern, "$1");
            var type = Enum.Parse<CommandType>(typeText);

            var parameter = Regex.Replace(input, pattern, "$2");

            return new(type, parameter);
        }

        throw new CommandException("The command provided is invalid.");
    }

    public override string ToString()
    {
        return $"{Type}|{Parameter}";
    }
}
