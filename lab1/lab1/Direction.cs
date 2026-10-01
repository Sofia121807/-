namespace lab1;

/// <summary>
/// Направление следования поезда
/// </summary>
public class Direction
{
    /// <summary>
    /// Уникальный идентификатор направления в системе.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название направления (маршрута).
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Протяженность направления в километрах.
    /// </summary>
    public int Distance { get; set; }

    /// <summary>
    /// Конструктор по умолчанию
    /// Инициализирует объект значениями по умолчанию
    /// </summary>
    public Direction()
    {

    }

    /// <summary>
    /// Конструктор с параметрами для полной инициализации объекта.
    /// </summary>
    /// <param name="id">Уникальный идентификатор направления.</param>
    /// <param name="name">Название направления.</param>
    /// <param name="distance">Протяженность направления в километрах.</param>
    public Direction(int id, string name, int distance)
    {
        if (name == null || name.Length == 0)
            throw new ArgumentNullException(nameof(name), "Название направления не может быть null или пустым");

        if (id <= 0)
            throw new ArgumentException("Id должен быть больше нуля", nameof(id));

        if (distance <= 0)
            throw new ArgumentException("Расстояние должно быть больше нуля", nameof(distance));

        Id = id;
        Name = name;
        Distance = distance;
    }

    /// <summary>
    /// Возвращает краткую текстовую информацию о направлении.
    /// </summary>
    /// <returns>Строка формата "Название (X км)".</returns>
    public string GetInfo()
    {
        return Name + " (" + Distance + " км)";
    }
}
