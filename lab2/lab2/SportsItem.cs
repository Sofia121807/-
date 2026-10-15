namespace lab2;

/// <summary>
/// Базовый класс спортивного инвентаря.
/// </summary>
public abstract class SportsItem
{
    /// <summary>
    /// Уникальный идентификатор инвентаря.
    /// </summary>
    private readonly int _id;

    /// <summary>
    /// Название инвентаря.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Цена инвентаря.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Вид спорта, к которому относится инвентарь.
    /// </summary>
    public SportType Sport { get; set; } = SportType.Football;

    /// <summary>
    /// Цена с учётом НДС 20%.
    /// </summary>
    public decimal PriceWithVat => Price * 1.2m;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="SportsItem"/>.
    /// </summary>
    /// <param name="id">Идентификатор инвентаря.</param>
    /// <param name="name">Название инвентаря.</param>
    /// <param name="price">Цена инвентаря.</param>
    public SportsItem(int id, string name, decimal price)
    {
        _id = id;
        Name = name;
        Price = price;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="SportsItem"/> с нулевым идентификатором и ценой.
    /// </summary>
    /// <param name="name">Название инвентаря.</param>
    public SportsItem(string name) : this(0, name, 0) { }

    /// <summary>
    /// Использует инвентарь по назначению.
    /// </summary>
    public abstract void Use();
}