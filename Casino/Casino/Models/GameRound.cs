using Casino.Interfaces;

namespace Casino.Models;

public class GameRound : IGameRound
{
    public int SecretNumber { get; private set; }
    public int MaxAttempts { get; private set; }
    public int CurrentAttempt { get; private set; }
    public bool IsGuessed { get; private set; }

    public GameRound (int maxNumber, int maxAttempts)
    {
        Random random = new Random();
        SecretNumber = random.Next(1,maxNumber + 1);
        MaxAttempts = maxAttempts;
        CurrentAttempt = 0;
        IsGuessed = false;
    }

    public int CheckGuess(int userGuess)
    {
        CurrentAttempt++;
        if (userGuess < SecretNumber) return 1;
        if (userGuess > SecretNumber) return -1;
        IsGuessed = true;
        return 0;
    }

}
