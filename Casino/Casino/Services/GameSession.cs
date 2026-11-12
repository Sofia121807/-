using Casino.Interfaces;
using Casino.Models;

namespace Casino.Services;

public class GameSession : IGameSession
{
    private IBetManager _betManager = new BetManager();
    private IWinCalculator _winCalculator = new WinCalculator();

    public int PlayRound(int currentBalance)
    {
        GameSettings currentSettings = GameSettings.ChooseDifficulty();
        int bet = _betManager.PlaceBet(currentBalance);
        IGameRound round = new GameRound(currentSettings.MaxNumber, currentSettings.MaxAttempts);


        Console.WriteLine($"\nЗагадано число от 1 до {currentSettings.MaxNumber}. У вас {currentSettings.MaxAttempts} попыток!");

        while (!round.IsGuessed && round.CurrentAttempt < round.MaxAttempts)
        {
            Console.WriteLine("Ваш вариант: ");
            string input = Console.ReadLine();

            try
            {
                int userGuess = Convert.ToInt32(input);
                int result = round.CheckGuess(userGuess);

                if (result == 1) Console.WriteLine($"Больше! Попытка: {round.CurrentAttempt}/{round.MaxAttempts}");
                else if (result == -1) Console.WriteLine($"Меньше! Попытка: {round.CurrentAttempt}/{round.MaxAttempts}");
                else
                {
                    Console.WriteLine($"\nВы угадали число {round.SecretNumber} с {round.CurrentAttempt} попытки!");

                    int winnings = _winCalculator.Calculate(bet, round.CurrentAttempt, round.MaxAttempts, currentSettings.WinMultiplier);

                    return winnings;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка. Введите целое число.");
            }

        }

        Console.WriteLine($"Попытки закончились. Было загадано число: {round.SecretNumber}");
        return -bet;
    }
}
