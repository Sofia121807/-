namespace Casino.Interfaces;

public interface IGameRound
{
    int SecretNumber { get;  } 
    int MaxAttempts { get; }
    int CurrentAttempt { get; }
    bool IsGuessed { get; }
    int CheckGuess(int userGuess);

}
