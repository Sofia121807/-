using System;
using System.Collections.Generic;
using System.IO;

namespace lab1;

/// <summary>
/// Главная программа: выбор источника данных и вызов  аналитических  методов
/// </summary>
class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите источник данных:");
        Console.WriteLine("1 — InMemoryRepository");
        Console.WriteLine("2 — CsvRepository");
        Console.Write("Ваш выбор: ");

        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
        {
            Console.WriteLine("Неверный ввод. Завершение работы.");
            return;
        }

        List<Direction> directions;
        List<Driver> drivers;
        List<Train> trains;

        try
        {
            switch (choice)
            {
                case 1:
                    InMemoryRepository memRepo = new InMemoryRepository();
                    directions = memRepo.GetDirections();
                    drivers = memRepo.GetDrivers();
                    trains = memRepo.GetTrains();
                    break;

                case 2:
                    string csvPath = Path.Combine(AppContext.BaseDirectory, "data");
                    if (!Directory.Exists(csvPath))
                    {
                        Console.WriteLine($"Ошибка: Папка '{csvPath}' не найдена.");
                        Console.WriteLine("Создайте папку 'data' рядом с exe-файлом и добавьте туда CSV-файлы.");
                        return;
                    }

                    CsvRepository csvRepo = new CsvRepository(csvPath);
                    directions = csvRepo.GetDirections();
                    drivers = csvRepo.GetDrivers();
                    trains = csvRepo.GetTrains();
                    break;

                default:
                    Console.WriteLine("Неверный выбор");
                    return;
            }
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Ошибка данных: {ex.Message}");
            return;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка данных: {ex.Message}");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при чтении данных: {ex.Message}");
            return;
        }

        Console.WriteLine("\n1. FindDriver(\"№123\"):");
        Driver foundDriver = FindDriver("№123", trains, drivers);
        if (foundDriver != null)
            Console.WriteLine("   " + foundDriver.GetInfo());
        else
            Console.WriteLine("null");

        Console.WriteLine("\n2. FindDirection(\"№123\"):");
        Direction foundDirection = FindDirection("№123", trains, directions);
        if (foundDirection != null)
            Console.WriteLine("   " + foundDirection.GetInfo());
        else
            Console.WriteLine("null");

        Console.WriteLine("\n3. GetTotalCapacity:");
        int? total = GetTotalCapacity(trains);
        Console.WriteLine("   " + total + " пассажиров");

        Console.WriteLine("\n4. GetDirectionsByTrainCount:");
        Dictionary<string, int> counts = GetDirectionsByTrainCount(trains, directions);
        foreach (KeyValuePair<string, int> pair in counts)
        {
            Console.WriteLine("   " + pair.Key + " — " + pair.Value);
        }

        Console.WriteLine("\n5. PrintAllTrains:");
        PrintAllTrains(trains, drivers, directions);

        Console.WriteLine("\nНе найдено: FindDriver(\"№999\"):");
        Driver notFound = FindDriver("№999", trains, drivers);
        if (notFound != null)
            Console.WriteLine("   " + notFound.GetInfo());
        else
            Console.WriteLine("null");

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    /// <summary>
    /// Находит машиниста, управляющего поездом с указанным номером.
    /// Сначала ищет поезд в списке по номеру, затем по DriverId находит запись в списке машинистов.
    /// </summary>
    /// <param name="trainNumber">Номер поезда для поиска (например, "№123").</param>
    /// <param name="trains">Список всех поездов.</param>
    /// <param name="drivers">Список всех машинистов.</param>
    /// <returns>Объект Driver, если поезд и машинист найдены; иначе null.</returns >
    static Driver? FindDriver(string trainNumber, List<Train> trains, List<Driver> drivers)
    {
        if (trainNumber == null || trains == null || drivers == null)
        {
            Console.WriteLine("Ошибка: один из параметров null");
            return null;
        }

        Train train = null;
        for (int i = 0; i < trains.Count; i++)
        {
            if (trains[i].Number == trainNumber)
            {
                train = trains[i];
                break;
            }
        }

        if (train == null) return null;

        for (int i = 0; i < drivers.Count; i++)
        {
            if (drivers[i].Id == train.DriverId)
            {
                return drivers[i];
            }
        }

        return null;
    }

    /// <summary>
    /// Находит направление следования для поезда с указанным номером.
    /// Сначала ищет поезд по номеру, затем по DirectionId находит запись в списке направлений.
    /// </summary>
    /// <param name="trainNumber">Номер поезда для поиска.</param>
    /// <param name="trains">Список всех поездов.</param>
    /// <param name="directions">Список всех направлений.</param>
    /// <returns>Объект Direction, если поезд и направление найдены; иначе null.</returns>
    static Direction? FindDirection(string trainNumber, List<Train> trains, List<Direction> directions)
    {
        if (trainNumber == null || trains == null || directions == null)
        {
            Console.WriteLine("Ошибка: один из параметров null");
            return null;
        }

        Train train = null;
        for (int i = 0; i < trains.Count; i++)
        {
            if (trains[i].Number == trainNumber)
            {
                train = trains[i];
                break;
            }
        }

        if (train == null) return null;

        for (int i = 0; i < directions.Count; i++)
        {
            if (directions[i].Id == train.DirectionId)
            {
                return directions[i];
            }
        }

        return null;
    }

    /// <summary>
    /// Вычисляет суммарную вместимость всех поездов в списке.
    /// </summary>
    /// <param name="trains">Список поездов.</param>
    /// <returns>Общее количество пассажирских мест.</returns>
    static int? GetTotalCapacity(List<Train> trains)
    {
        if (trains == null) return null;
        int total = 0;
        for (int i = 0; i < trains.Count; i++)
        {
            total += trains[i].Capacity;
        }
        return total;
    }

    /// <summary>
    /// Формирует словарь, где ключ — название направления,
    /// а значение — количество поездов, следующих в этом направлении.
    /// </summary>
    /// <param name="trains">Список поездов.</param>
    /// <param name="directions">Список направлений.</param>
    /// <returns>Словарь {Название направления: Количество поездов}.</returns>
    static Dictionary<string, int>? GetDirectionsByTrainCount(List<Train> trains, List<Direction> directions)
    {
        if (trains == null || directions == null)
        {
            Console.WriteLine("Ошибка: один из параметров null");
            return null;
        }

        Dictionary<string, int> result = new Dictionary<string, int>();

        for (int i = 0; i < trains.Count; i++)
        {
            string dirName = null;
            for (int j = 0; j < directions.Count; j++)
            {
                if (directions[j].Id == trains[i].DirectionId)
                {
                    dirName = directions[j].Name;
                    break;
                }
            }

            if (dirName == null) continue;

            if (result.ContainsKey(dirName))
            {
                result[dirName]++;
            }
            else
            {
                result[dirName] = 1;
            }
        }

        return result;
    }

    /// <summary>
    /// Выводит подробную информацию о каждом поезде: данные о составе,
    /// имя машиниста и название направления.
    /// </summary>
    /// <param name="trains">Список поездов.</param>
    /// <param name="drivers">Список машинистов.</param>
    /// <param name="directions">Список направлений.</param>
    static void PrintAllTrains(List<Train> trains, List<Driver> drivers, List<Direction> directions)
    {
        if (trains == null || drivers == null || directions == null)
        {
            Console.WriteLine("Ошибка: один из параметров null");
            return;
        }

        for (int i = 0; i < trains.Count; i++)
        {
            Train t = trains[i];

            string driverName = "—";
            for (int j = 0; j < drivers.Count; j++)
            {
                if (drivers[j].Id == t.DriverId)
                {
                    driverName = drivers[j].FullName;
                    break;
                }
            }

            string dirName = "—";
            for (int j = 0; j < directions.Count; j++)
            {
                if (directions[j].Id == t.DirectionId)
                {
                    dirName = directions[j].Name;
                    break;
                }
            }

            Console.WriteLine("   \"" + t.GetInfo() + "\" — машинист " + driverName + ", направление \"" + dirName + "\"");
        }
    }
}
