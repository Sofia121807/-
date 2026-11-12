using Casino.Interfaces;

namespace Casino.Services;

public class MenuService : IMenuService
{
    public string ShowMainMenu()
    {
        Console.WriteLine("\nГлавное меню: ");
        Console.WriteLine("1. Начать игру");
        Console.WriteLine("2. Показать историю игр");
        Console.WriteLine("3. Выйти из казино");
        Console.WriteLine("Ваш выбор: ");

        return Console.ReadLine();
    }
}
