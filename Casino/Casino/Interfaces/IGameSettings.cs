namespace Casino.Interfaces;

public interface IGameSettings
{
    int MaxNumber { get; set; }
    int MaxAttempts { get; set; }
    int WinMultiplier { get; set; }
}
