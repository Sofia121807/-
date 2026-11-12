using Casino.Interfaces;

namespace Casino;

public class WinCalculator : IWinCalculator
{
    public int Calculate(int bet, int attempts, int maxAttempts, int winMultiplier)
    {
        if (attempts == 1)  return bet * (winMultiplier * 2);
        else if (attempts <= maxAttempts / 2) return bet * winMultiplier;
        else return bet;
    }

}
