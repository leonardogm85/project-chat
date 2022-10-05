namespace ChatCommon.Events;

public sealed class ErrorChangedEventArgs : EventArgs
{
    public string PropertyName { get; }

    public ErrorChangedEventArgs(string propertyName)
    {
        PropertyName = propertyName;
    }
}
