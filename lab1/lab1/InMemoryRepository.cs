namespace lab1;

/// <summary>
/// Репозиторий с тестовыми данными в памяти
/// </summary>
public class InMemoryRepository
{
    /// <summary>
    /// Список направлений следования поездов.
    /// </summary>
    private List<Direction> _directions;
    /// <summary>
    /// Список машинистов.
    /// </summary>
    private List<Driver> _drivers;
    /// <summary>
    /// Список поездов.
    /// </summary>
    private List<Train> _trains;

    /// <summary>
    /// Конструктор без параметров, инициализирует списки тестовыми данными
    /// 5 направлений, 5 машинистов и 5 поездов.
    /// </summary>
    public InMemoryRepository()
    {
        _directions = new List<Direction>
        {
            new Direction(1, "Москва-Питер", 650),
            new Direction(2, "Москва-Казань", 800),
            new Direction(3, "Москва-Сочи", 1600),
            new Direction(4, "Москва-Екатеринбург", 1800),
            new Direction(5, "Москва-Новосибирск", 3300)
        };

        _drivers = new List<Driver>
        {
            new Driver(1, "Иванов И.И.", 15, "A"),
            new Driver(2, "Петров П.П.", 8, "B"),
            new Driver(3, "Сидоров С.С.", 20, "A"),
            new Driver(4, "Кузнецов К.К.", 5, "C"),
            new Driver(5, "Смирнова А.А.", 12, "A")
        };

        _trains = new List<Train>
        {
            new Train(1, "№123", 1, 1, 1000, "Скоростной"),
            new Train(2, "№456", 2, 2, 800, "Пассажирский"),
            new Train(3, "№789", 1, 3, 1200, "Скоростной"),
            new Train(4, "№101", 1, 4, 600, "Пассажирский"),
            new Train(5, "№202", 2, 5, 1400, "Скоростной")
        };
    }

    /// <summary>
    /// Возвращает список всех направлений.
    /// </summary>
    public List<Direction> GetDirections() { return _directions; }

    /// <summary>
    /// Возвращает список всех машинистов.
    /// </summary>
    public List<Driver> GetDrivers() { return _drivers; }

    /// <summary>
    /// Возвращает список всех поездов.
    /// </summary>
    public List<Train> GetTrains() { return _trains; }
}
