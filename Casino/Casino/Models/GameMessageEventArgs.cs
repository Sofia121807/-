namespace Casino.Models;

internal class GameMessageEventArgs : EventArgs
{
    public string Message { get; set; }

    public GameMessageEventArgs(string message)
    {
        Message = message;
    }
}
