namespace Casino;

internal class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();

        Console.WriteLine("Добро пожаловать в Казино!");
        int balance = 10000;
        Console.WriteLine($"Ваш стартовый баланс: {balance} рублей");

        bool keepPlayng = true;

        while (keepPlayng)
        {
            if (balance <= 0)
            {
                Console.WriteLine("Ваш баланс отрицательный! Игра окончена!");
                break;
            }

            int bet = 0; //ставка
            bool validBet = false;

            while (!validBet)
            {
                Console.WriteLine($"Сделайте вашу ставку (баланс {balance} рублей): ");
                string input = Console.ReadLine();

                try
                {
                    bet = Convert.ToInt32(input);
                    if (bet <= 0 || bet > balance) Console.WriteLine($"Ставка должна быть больше нуля и не превышать ваш баланс (Баланс: {balance})!");
                    else validBet = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка. Введите целое число для ставки!");
                }
            }

            int secretNumber = random.Next(1, 101);
            int attempts = 0;
            bool isGuessed = false;

            Console.WriteLine("Загадано число от 1 до 100. У вас 10 попыток!");
            Console.WriteLine(" ");

            while (!isGuessed && attempts < 10)
            {
                Console.WriteLine("Ваш вариант: ");
                string input = Console.ReadLine();

                try
                {
                    int userGuess = Convert.ToInt32(input);
                    attempts++;

                    if (userGuess < secretNumber) Console.WriteLine($"Больше! Попытка: {attempts}/10");
                    else if (userGuess > secretNumber) Console.WriteLine($"Меньше! Попытка: {attempts}/10");
                    else
                    {
                        isGuessed = true;
                        Console.WriteLine($"\nВы угадали число {secretNumber} с {attempts} попытки!");

                        int winnings = 0;
                        if (attempts == 1) winnings = bet * 10;
                        else if (attempts <= 5) winnings = bet * 2;
                        else winnings = bet;

                        balance += winnings;
                        Console.WriteLine($"Ваш выигрыш составил: {winnings} рублей. \nВаш баланс: {balance} рублей.");

                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка. Введите целое число.");
                }

            }

            if (!isGuessed)
            {
                Console.WriteLine($"Попытки закончились. Было загадано число: {secretNumber}");
                balance -= bet;
            }
            Console.WriteLine($"\nВаш баланс: {balance}");

            if (balance > 0)
            {
                Console.WriteLine("Хотите сыграть еще раз? (y/n): ");
                string answer = Console.ReadLine();
                if (answer != "Y" && answer != "y") keepPlayng = false;
            }
        }
        Console.WriteLine("Спасибо за игру! До встречи в нашем казино))");
    }
}
