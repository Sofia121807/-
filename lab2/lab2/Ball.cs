namespace lab2;

/// <summary>
/// Мяч — надувной спортивный инвентарь.
/// </summary>
public class Ball : SportsItem, IInflatable
{
    /// <summary>
    /// Диаметр мяча в сантиметрах.
    /// </summary>
    public int Diameter { get; set; }

    /// <summary>
    /// Признак того, что мяч большой (диаметр больше 22 см).
    /// </summary>
    public bool IsBig => Diameter > 22;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Ball"/>.
    /// </summary>
    /// <param name="id">Идентификатор инвентаря.</param>
    /// <param name="name">Название мяча.</param>
    /// <param name="price">Цена мяча.</param>
    /// <param name="diameter">Диаметр мяча в сантиметрах.</param>
    public Ball(int id, string name, decimal price, int diameter) : base(id, name, price)
    {
        Diameter = diameter;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Ball"/> по названию.
    /// </summary>
    /// <param name="name">Название мяча.</param>
    public Ball(string name) : this(0, name, 0, 0) { }

    /// <inheritdoc/>
    public override void Use()
    {
        Console.WriteLine($"{Name} used.");
    }

    /// <inheritdoc/>
    public void Inflate()
    {
        Console.WriteLine($"{Name} inflated.");
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[Ball] {Name}, {Price}, sport: {Sport}";
    }
}