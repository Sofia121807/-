using System;

namespace Casino;

internal class CasinoGame
{
    private Random random = new Random();
    private int _balance = 10000;
    private StatisticManager _statisticManager = new StatisticManager();

    public event EventHandler<GameMessageEventArgs>? OnMessage;

    public void Start()
    {
        OnGameMessage("Добро пожаловать в Казино!");

        while (true)
        {
            OnGameMessage("\nГлавное меню: ");
            OnGameMessage("1. Начать игру");
            OnGameMessage("2. Показать историю игр");
            OnGameMessage("3. Выйти из казино");

            Console.WriteLine("Ваш выбор: ");
            string startChoice = Console.ReadLine();

            if (startChoice == "2")
            {
                _statisticManager.ShowHistory();
                continue;
            }
            else if (startChoice == "3")
            {
                OnGameMessage("Спасибо за игру! До встречи в нашем казино))");
                return;
            }
            else if (startChoice == "1")
            {
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

                                _statisticManager.SaveResult($"Победа. Попыток: {attempts}. Выигрыш: {winnings} рублей.");
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

                        _statisticManager.SaveResult($"Проигрыш. Ставка: {bet} рублей.");
                    }
                    OnGameMessage($"\nВаш баланс: {_balance}");

                    if (_balance > 0)
                    {
                        Console.WriteLine("Хотите сыграть еще раз? (y/n): ");
                        string answer = Console.ReadLine();
                        if (answer != "Y" && answer != "y") break;
                    }
                }
            }

            else OnGameMessage("Ошибка. Введите 1, 2 или 3.");
        }
    }

    protected virtual void OnGameMessage(string message)
    {
        OnMessage?.Invoke(this, new GameMessageEventArgs(message));
    }
}