namespace lab2;

/// <summary>
/// Спасательный круг — надувной инвентарь для плавания.
/// </summary>
public class SwimRing : SportsItem, IInflatable
{
    /// <summary>
    /// Максимально допустимый вес пользователя в килограммах.
    /// </summary>
    public int MaxWeight { get; set; }

    /// <summary>
    /// Признак того, что круг рассчитан на взрослого (вес больше 60 кг).
    /// </summary>
    public bool IsAdult => MaxWeight > 60;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="SwimRing"/>.
    /// </summary>
    /// <param name="id">Идентификатор инвентаря.</param>
    /// <param name="name">Название круга.</param>
    /// <param name="price">Цена круга.</param>
    /// <param name="maxWeight">Максимальный вес пользователя.</param>
    public SwimRing(int id, string name, decimal price, int maxWeight) : base(id, name, price)
    {
        MaxWeight = maxWeight;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="SwimRing"/> по названию.
    /// </summary>
    /// <param name="name">Название круга.</param>
    public SwimRing(string name) : this(0, name, 0, 0) { }

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
        return $"[SwimRing] {Name}, {Price}, sport: {Sport}, max weight: {MaxWeight}";
    }
}