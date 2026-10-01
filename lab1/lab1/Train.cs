namespace lab1;

/// <summary>
/// Поезд, следующий по направлению под управлением машиниста
/// </summary>
public class Train
{
    /// <summary>
    /// Уникальный идентификатор поезда в системе.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Номер поезда (например, "№123" или "056А").
    /// По умолчанию инициализируется пустой строкой.
    /// </summary>
    public string Number { get; set; } = "";
    /// <summary>
    /// Идентификатор направления, по которому следует поезд.
    /// Используется для связи с объектом класса Direction.
    /// </summary>
    public int DirectionId { get; set; }
    /// <summary>
    /// Идентификатор машиниста, управляющего данным поездом.
    /// Используется для связи с объектом класса Driver.
    /// </summary>
    public int DriverId { get; set; }
    /// <summary>
    /// Вместимость поезда (количество пассажирских мест).
    /// </summary>
    public int Capacity { get; set; }
    /// <summary>
    /// Тип поезда (например, "Скоростной", "Пассажирский", "Грузовой").
    /// По умолчанию инициализируется пустой строкой.
    /// </summary>
    public string Type { get; set; } = "";

    /// <summary>
    /// Конструктор по умолчанию
    /// Инициализирует объект значениями по умолчанию
    /// </summary>
    public Train()
    {
    }

    /// <summary>
    /// Конструктор с параметрами для полной инициализации объекта.
    /// Все параметры проверяются на корректность.
    /// </summary>
    /// <param name="id">Уникальный идентификатор поезда.</param>
    /// <param name="number">Номер поезда.</param>
    /// <param name="directionId">Идентификатор направления.</param>
    /// <param name="driverId">Идентификатор машиниста.</param>
    /// <param name="capacity">Вместимость поезда.</param>
    /// <param name="type">Тип поезда.</param>
    /// <exception cref="ArgumentNullException">Выбрасывается, если number или type равен null или пуст.</exception>
    /// <exception cref="ArgumentException">Выбрасывается, если id, directionId, driverId <= 0 или capacity <= 0.</exception>
    public Train(int id, string number, int directionId, int driverId, int capacity, string type)
    {
        if (number == null || number.Length == 0)
            throw new ArgumentNullException(nameof(number), "Номер поезда не может быть null или пустым");

        if (type == null || type.Length == 0)
            throw new ArgumentNullException(nameof(type), "Тип поезда не может быть null или пустым");

        if (id <= 0)
            throw new ArgumentException("Id должен быть больше нуля", nameof(id));

        if (directionId <= 0)
            throw new ArgumentException("DirectionId должен быть больше нуля", nameof(directionId));

        if (driverId <= 0)
            throw new ArgumentException("DriverId должен быть больше нуля", nameof(driverId));

        if (capacity <= 0)
            throw new ArgumentException("Вместимость должна быть больше нуля", nameof(capacity));

        Id = id;
        Number = number;
        DirectionId = directionId;
        DriverId = driverId;
        Capacity = capacity;
        Type = type;
    }


    /// <summary>
    /// Флаг, указывающий на то, что поезд является скоростным
    /// Возвращает true, если свойство Type равно строке "Скоростной"
    /// </summary>
    public bool IsFast
    {
        get { return Type == "Скоростной"; }
    }

    /// <summary>
    /// Возвращает краткую текстовую информацию о поезде.
    /// </summary>
    /// <returns>Строка формата "Номер (Тип, X мест)".</returns>
    public string GetInfo()
    {
        return Number + " (" + Type + ", " + Capacity + " мест)";
    }
}
