namespace lab2;

/// <summary>
/// Ракетка — спортивный инвентарь для тенниса.
/// </summary>
public class Racket : SportsItem
{
    /// <summary>
    /// Натяжение струн в фунтах.
    /// </summary>
    public int StringTension { get; set; }

    /// <summary>
    /// Признак того, что струны натянуты сильно (больше 25 фунтов).
    /// </summary>
    public bool IsTight => StringTension > 25;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Racket"/>.
    /// </summary>
    /// <param name="id">Идентификатор инвентаря.</param>
    /// <param name="name">Название ракетки.</param>
    /// <param name="price">Цена ракетки.</param>
    /// <param name="stringTension">Натяжение струн в фунтах.</param>
    public Racket(int id, string name, decimal price, int stringTension) : base(id, name, price)
    {
        StringTension = stringTension;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Racket"/> по названию.
    /// </summary>
    /// <param name="name">Название ракетки.</param>
    public Racket(string name) : this(0, name, 0, 0) { }

    /// <inheritdoc/>
    public override void Use()
    {
        Console.WriteLine($"{Name} used.");
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[Racket] {Name}, {Price}, sport: {Sport}, tension: {StringTension}";
    }
}