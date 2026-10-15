namespace pr2;

/// <summary>
/// Счёт подарочной карты с возможностью ежемесячного автоматического пополнения.
/// </summary>
public class GiftCartAccount : BankAccount
{
    /// <summary>
    /// Сумма ежемесячного автоматического пополнения. Значение 0 отключает пополнение.
    /// </summary>
    private readonly decimal _monthlyDeposit = 0m;

    //_monthlyDeposit  - параметр по умолчанию (прнимает 0),
    // при создании new GiftCartAccount("Yana", 1000); => monthlyDeposit = 0
    //new GiftCartAccount("Yana",1000, 5000); => monthlyDeposit = 5000

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="GiftCartAccount"/>.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    /// <param name="monthlyDeposit">Сумма ежемесячного автоматического пополнения.</param>
    public GiftCartAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name, initialBalance)
        => _monthlyDeposit = monthlyDeposit;

    /// <summary>
    /// Вносит ежемесячное автоматическое пополнение, если его сумма задана.
    /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (_monthlyDeposit != 0)
        {
            MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add monthly deposit");
        }
    }

    // base.ToString() - вызов базовой реализации => реализация из родительского класса BankAccount
    /// <summary>
    /// Возвращает текстовое представление счёта с указанием суммы ежемесячного пополнения.
    /// </summary>
    /// <returns>
    /// Строка, полученная из <see cref="BankAccount.ToString"/>, с добавлением суммы ежемесячного пополнения.
    /// </returns>
    public override string ToString() => base.ToString() + $"  Monthly deposit: {_monthlyDeposit}";
}