using System.Net.NetworkInformation;
using System.Text;

namespace pr2;

//BankAccount - потомок класс object => можно переопределить виртуалиные методы, находящиеся в object
/// <summary>
/// Банковский счёт, поддерживающий операции пополнения и снятия
/// </summary>
public class BankAccount
{
    /// <summary>
    /// Минимально допустимый баланс счёта.
    /// </summary>
    private readonly decimal _minimumBalance;

    /// <summary>
    /// Начальное значение для генерации уникальных номеров счетов
    /// </summary>
    static private int s_accountNumberSeed = 1000000000;

    /// <summary>
    /// Уникальный номер счёта
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// Имя владельца счёта
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    /// Текущий баланс счёта, рассчитанный на основе всех транзакций
    /// </summary>
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }
            return balance;
        }
    }

    /// <summary>
    /// Журнал всех операций по счёту.
    /// </summary>
    private List<Transactionn> _allTransactions = new List<Transactionn>();

    /// <summary>
    /// Статус клиента банка.
    /// </summary>
    public AccountStatus Status { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="BankAccount"/> со статусом Regular.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    public BankAccount(string name, decimal initialBalance)
        : this(name, initialBalance, 0, AccountStatus.Regular) { }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="BankAccount"/> с заданным минимальным балансом.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    /// <param name="minimumBalance">Минимально допустимый баланс счёта.</param>
    /// <param name="status">Статус клиента банка.</param>
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance,
                       AccountStatus status = AccountStatus.Regular)
    {
        Owner = name;
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
        _minimumBalance = minimumBalance;
        Status = status;

        if (initialBalance > 0)
            MakeDeposit(initialBalance, DateTime.UtcNow, "Initial Balance");
    }

    /// <summary>
    /// Пополняет счёт на указанную сумму.
    /// </summary>
    /// <param name="amount">Сумма пополнения. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <param name="transferId">
    /// Идентификатор перевода, если операция — часть перевода между счетами.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Бросается, если <paramref name="amount"/> меньше или равен нулю.
    /// </exception>
    public void MakeDeposit(decimal amount, DateTime date, string note, Guid? transferId = null)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive"); // исключение если сумма отрицательная
        }
        var deposit = new Transactionn(amount, date, note, transferId);
        _allTransactions.Add(deposit);

        AuditLog.Record(Number, OperationType.Deposit, amount, date, transferId);
    }

    /// <summary>
    /// Списывает указанную сумму со счёта с учётом минимального баланса.
    /// </summary>
    /// <param name="amount">Сумма списания. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <param name="transferId">
    /// Идентификатор перевода, если операция — часть перевода между счетами.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Бросается, если <paramref name="amount"/> меньше или равен нулю.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Бросается, если после списания баланс опустится ниже минимально допустимого.
    /// </exception>
    public void MakeWithdrawal(decimal amount, DateTime date, string note, Guid? transferId = null)
    {
        //if (amount <= 0)
        //{
        //    throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive ");
        //}
        //if (Balance < amount)
        //{
        //    throw new InvalidOperationException("Not sufficient rubls for this withdawal");
        //}
        //var withdawal = new Transactionn(-amount, date, note);
        //_allTransactions.Add(withdawal);

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Transactionn? overdraftTransactionn = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transactionn? withdrawal = new(-amount, date, note, transferId);
        _allTransactions.Add(withdrawal);

        AuditLog.Record(Number, OperationType.Withdrawal, amount, date, transferId);

        if (overdraftTransactionn is not null)
        {
            _allTransactions.Add(overdraftTransactionn);
            AuditLog.Record(Number, OperationType.Withdrawal, -overdraftTransactionn.Amount, date, transferId);
        }
    }

    /// <summary>
    /// Проверяет, допустимо ли списание, не нарушающее минимальный баланс.
    /// </summary>
    /// <param name="isOverdrawn">
    /// Признак того, что баланс после списания опустится ниже минимально допустимого.
    /// </param>
    /// <returns>
    /// Транзакция за овердрафт для добавления в историю; значение по умолчанию, если овердрафта нет.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Бросается, если списание недопустимо при текущем минимальном балансе.
    /// </exception>
    protected virtual Transactionn? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException("Not sufficient rubls for this withdrawal");
        }
        return default;
    }

    /// <summary>
    /// Формирует текстовый отчёт по всем операциям счёта.
    /// </summary>
    /// <returns>
    /// Строка, содержащая дату, сумму и комментарий каждой транзакции.
    /// </returns>
    public string GetAccountHistory()
    {
        var report = new StringBuilder();

        decimal balance = 0;
        report.AppendLine("Data\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.date.ToShortDateString()}\t" + $"{item.Amount}\t{item.Note}");
        }
        return report.ToString();
    }

    // Ключевое слово virtual позволяет в дочернем классе предоставить другую реализацию метода PerformMonthAndTransactions
    /// <summary>
    /// Начисляет комиссию, проценты и кэшбэк согласно статусу клиента.
    /// </summary>
    public virtual void PerformMonthAndTransactions()
    {
        ApplyStatusBenefits();
    }

    /// <summary>
    /// Применяет льготы и комиссии согласно статусу клиента.
    /// </summary>
    protected virtual void ApplyStatusBenefits()
    {
        switch (Status)
        {
            case AccountStatus.Regular:
                if (Balance > 0)
                    MakeWithdrawal(50m, DateTime.UtcNow, "Monthly fee (Regular)");
                break;

            case AccountStatus.Premium:
                if (Balance > 10_000m)
                {
                    decimal interest = (Balance - 10_000m) * 0.01m;
                    MakeDeposit(interest, DateTime.UtcNow, "Premium interest");
                }
                break;

            case AccountStatus.VIP:
                if (Balance > 10_000m)
                {
                    decimal interest = (Balance - 10_000m) * 0.03m;
                    MakeDeposit(interest, DateTime.UtcNow, "VIP interest");
                }
                decimal cashback = GetMonthlyWithdrawalsTotal() * 0.05m;
                if (cashback > 0)
                    MakeDeposit(cashback, DateTime.UtcNow, "VIP cashback");
                break;
        }
    }

    /// <summary>
    /// Возвращает суммарную сумму списаний за текущий месяц.
    /// </summary>
    /// <returns>Сумма списаний (положительное число).</returns>
    protected decimal GetMonthlyWithdrawalsTotal()
    {
        var now = DateTime.UtcNow;
        decimal total = 0m;

        foreach (var t in _allTransactions)
        {
            if (t.Amount < 0
                && t.date.Year == now.Year
                && t.date.Month == now.Month)
            {
                total += -t.Amount;
            }
        }

        return total;
    }


    // переопределяем метод, который унаследовали от object
    // этот метод должен возвращать строку с состоянием объекта

    //public override string ToString()
    //{
    //    return $"Type:{GetType().Name}\tOwner: {Owner}\tNumber of account:{Number}";
    //}
    /// <summary>
    /// Возвращает текстовое представление счёта: тип, владельца и номер.
    /// </summary>
    /// <returns>
    /// Строка вида <c>Type:BankAccount  Owner: yana  Number of account:1000000000</c>.
    /// </returns>
    public override string ToString()
        => $"Type:{GetType().Name}\tOwner: {Owner}\tNumber of account:{Number}";

}