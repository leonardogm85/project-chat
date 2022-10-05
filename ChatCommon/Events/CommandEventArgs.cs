namespace ChatCommon.Events;

public abstract class CommandEventArgs : EventArgs
{
    public bool InputHandlerStopped { get; set; }
}
