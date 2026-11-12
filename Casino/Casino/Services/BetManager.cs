using Casino.Interfaces;

namespace Casino.Services;

public class BetManager : IBetManager
{
    public int PlaceBet (int balance)
    {
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
        return bet;
    }
}
