namespace lab2;

/// <summary>
/// Точка входа в программу.
/// </summary>
public class Program
{
    /// <summary>
    /// Демонстрирует полиморфизм на коллекции спортивного инвентаря.
    /// </summary>
    /// <param name="args">Аргументы командной строки.</param>
    static void Main(string[] args)
    {

        var ball = new Ball(1, "Soccer ball", 2000m, 22) { Sport = SportType.Football };
        var racket = new Racket(2, "Wilson", 15000m, 25) { Sport = SportType.Tennis };
        var ring = new SwimRing(3, "Inflatable ring", 800m, 80) { Sport = SportType.Swimming };

        var items = new List<SportsItem> { ball, racket, ring };

        foreach (SportsItem item in items)
        {
            Console.WriteLine(item);
            Console.WriteLine($"  price with VAT: {item.PriceWithVat}");

            item.Use();

            if (item is IInflatable inflatable)
            {
                inflatable.Inflate();
            }
            else
            {
                Console.WriteLine($"{item.Name} cannot be inflated.");
            }

            Console.WriteLine(new string('-', 40));
        }
    }
}
