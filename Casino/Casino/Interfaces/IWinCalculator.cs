namespace Casino.Interfaces;

public interface IWinCalculator
{
    int Calculate (int bet, int attempts, int maxAttempts, int winMultiplier);
}
