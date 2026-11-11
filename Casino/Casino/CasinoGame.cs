using System;

namespace Casino;

internal class CasinoGame
{
    private Random random = new Random();
    private int _balance = 10000;

    public event EventHandler<GameMessageEventArgs>? OnMessage;

    public void Start()
    {
        OnGameMessage("Добро пожаловать в Казино!");
        OnGameMessage($"Ваш стартовый баланс: {_balance} рублей");

        bool keepPlayng = true;

        while (keepPlayng)
        {
            if (_balance <= 0)
            {
                OnGameMessage("Ваш баланс отрицательный! Игра окончена!");
                break;
            }

            GameSettings currentSettings = GameSettings.ChooseDifficulty();

            int bet = 0; //ставка
            bool validBet = false;

            while (!validBet)
            {
                Console.WriteLine($"Сделайте вашу ставку (баланс {_balance} рублей): ");
                string input = Console.ReadLine();

                try
                {
                    bet = Convert.ToInt32(input);
                    if (bet <= 0 || bet > _balance) OnGameMessage($"Ставка должна быть больше нуля и не превышать ваш баланс (Баланс: {_balance})!");
                    else validBet = true;
                }
                catch (FormatException)
                {
                    OnGameMessage("Ошибка. Введите целое число для ставки!");
                }
            }

            int secretNumber = random.Next(1, currentSettings.MaxNumber + 1);
            int attempts = 0;
            bool isGuessed = false;

            OnGameMessage($"Загадано число от 1 до {currentSettings.MaxNumber}. У вас {currentSettings.MaxAttempts} попыток!");

            while (!isGuessed && attempts < currentSettings.MaxAttempts)
            {
                Console.WriteLine("Ваш вариант: ");
                string input = Console.ReadLine();

                try
                {
                    int userGuess = Convert.ToInt32(input);
                    attempts++;

                    if (userGuess < secretNumber) OnGameMessage($"Больше! Попытка: {attempts}/{currentSettings.MaxAttempts}");
                    else if (userGuess > secretNumber) OnGameMessage($"Меньше! Попытка: {attempts}/{currentSettings.MaxAttempts}");
                    else
                    {
                        isGuessed = true;
                        OnGameMessage($"\nВы угадали число {secretNumber} с {attempts} попытки!");

                        int winnings = 0;
                        if (attempts == 1) winnings = bet * (currentSettings.WinMultiplier * 2);
                        else if (attempts <= currentSettings.MaxAttempts / 2) winnings = bet * currentSettings.WinMultiplier;
                        else winnings = bet;

                        _balance += winnings;
                        OnGameMessage($"Ваш выигрыш составил: {winnings} рублей. \nВаш баланс: {_balance} рублей.");
                    }
                }
                catch (FormatException)
                {
                    OnGameMessage("Ошибка. Введите целое число.");
                }

            }

            if (!isGuessed)
            {
                OnGameMessage($"Попытки закончились. Было загадано число: {secretNumber}");
                _balance -= bet;
            }
            OnGameMessage($"\nВаш баланс: {_balance}");

            if (_balance > 0)
            {
                Console.WriteLine("Хотите сыграть еще раз? (y/n): ");
                string answer = Console.ReadLine();
                if (answer != "Y" && answer != "y") keepPlayng = false;
            }
        }
        OnGameMessage("Спасибо за игру! До встречи в нашем казино))");
    }

    protected virtual void OnGameMessage(string message)
    {
        OnMessage?.Invoke(this, new GameMessageEventArgs(message));
    }
}