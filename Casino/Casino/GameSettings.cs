namespace Casino;

public class GameSettings
{
    public int MaxNumber { get; set; }
    public int MaxAttempts { get; set; }
    public int WinMultiplier { get; set; }

    public static GameSettings ChooseDifficulty()
    {
        while (true)
        {
            Console.WriteLine("\nВыберите уровень сложности: ");
            Console.WriteLine("1. Легкий (1-10, 5 попыток, выигрыш х2)");
            Console.WriteLine("2. Средний (1-100, 7 попыток, выигрыш х5)");
            Console.WriteLine("3. Сложный (1-1000, 10 попыток, выигрыш х10)");
            Console.WriteLine("Ваш выбор (1-3): ");

            string choice = Console.ReadLine();

            if (choice == "1") return new GameSettings { MaxNumber = 10, MaxAttempts = 5, WinMultiplier = 2 };
            else if (choice == "2") return new GameSettings { MaxNumber = 100, MaxAttempts = 7, WinMultiplier = 5 };
            else if (choice == "3") return new GameSettings { MaxNumber = 1000, MaxAttempts = 10, WinMultiplier = 10 };
            else Console.WriteLine("Ошибка! Введите число 1,2 или 3.");
        }
    }
}
