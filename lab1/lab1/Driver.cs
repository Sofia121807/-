namespace lab1;

/// <summary>
/// Машинист поезда
/// </summary>
public class Driver
{
    /// <summary>
    /// Уникальный идентификатор машиниста в системе.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Полное имя машиниста.
    /// По умолчанию инициализируется пустой строкой.
    /// </summary>
    public string FullName { get; set; } = "";
    /// <summary>
    /// Стаж работы машиниста в годах.
    /// </summary>
    public int Experience { get; set; }
    /// <summary>
    /// Номер водительской лицензии или удостоверения машиниста.
    /// По умолчанию инициализируется пустой строкой.
    /// </summary>
    public string License { get; set; } = "";

    /// <summary>
    /// Конструктор по умолчанию.
    /// Инициализирует объект со значениями по умолчанию (0 для чисел, пустая строка для текста).
    /// </summary>
    public Driver()
    {

    }

    /// <summary>
    /// Конструктор с параметрами для быстрой инициализации всех свойств объекта.
    /// Все параметры проверяются на корректность.
    /// </summary>
    /// <param name="id">Уникальный идентификатор машиниста.</param>
    /// <param name="fullName">Полное имя машиниста.</param>
    /// <param name="experience">Стаж работы в годах.</param>
    /// <param name="license">Номер лицензии.</param>
    /// <exception cref="ArgumentNullException">Выбрасывается, если fullName или license равен null или пуст.</exception>
    /// <exception cref="ArgumentException">Выбрасывается, если id <= 0 или experience < 0.</exception>
    public Driver(int id, string fullName, int experience, string license)
    {
        if (fullName == null || fullName.Length == 0)
            throw new ArgumentNullException(nameof(fullName), "Имя машиниста не может быть null или пустым");

        if (license == null || license.Length == 0)
            throw new ArgumentNullException(nameof(license), "Лицензия не может быть null или пустой");

        if (id <= 0)
            throw new ArgumentException("Id должен быть больше нуля", nameof(id));

        if (experience < 0)
            throw new ArgumentException("Стаж не может быть отрицательным", nameof(experience));

        Id = id;
        FullName = fullName;
        Experience = experience;
        License = license;
    }


    /// <summary>
    /// Флаг, указывающий на высокую квалификацию машиниста
    /// Возвращает true, если стаж работы превышает 10 лет
    /// </summary>
    public bool IsExperienced
    {
        get { return Experience > 10; }
    }

    /// <summary>
    /// Возвращает краткую текстовую информацию о машинисте: имя и стаж работы
    /// </summary>
    /// <returns>Строка формата "Имя (X лет стажа)".</returns>
    public string GetInfo()
    {
        return FullName + " (" + Experience + " лет стажа)";
    }
}
