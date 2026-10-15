using System;
using System.Collections.Generic;
using System.Text;

namespace pr2;

/// <summary>
/// Тип банковской операции для журнала аудита.
/// </summary>
public enum OperationType
{
    /// <summary>
    /// Пополнение счёта.
    /// </summary>
    Deposit,

    /// <summary>
    /// Списание со счёта.
    /// </summary>
    Withdrawal,

    /// <summary>
    /// Начисление процентов.
    /// </summary>
    Interest
}

/// <summary>
/// Одна запись журнала аудита.
/// </summary>
/// <param name="AccountNumber">Номер счёта.</param>
/// <param name="Type">Тип операции.</param>
/// <param name="Amount">Сумма операции.</param>
/// <param name="Date">Дата операции.</param>
/// <param name="TransferId">Идентификатор перевода, если операция парная.</param>
public record AuditEntry(
    string AccountNumber,
    OperationType Type,
    decimal Amount,
    DateTime Date,
    Guid? TransferId = null);

/// <summary>
/// Журнал аудита всех банковских операций.
/// </summary>
public static class AuditLog
{
    /// <summary>
    /// Внутреннее хранилище записей аудита.
    /// </summary>
    private static readonly List<AuditEntry> s_entries = new List<AuditEntry>();

    /// <summary>
    /// Записывает операцию в журнал аудита.
    /// </summary>
    /// <param name="accountNumber">Номер счёта.</param>
    /// <param name="type">Тип операции.</param>
    /// <param name="amount">Сумма операции.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="transferId">Идентификатор перевода, если операция парная.</param>
    public static void Record(string accountNumber, OperationType type, decimal amount, DateTime date, Guid? transferId = null)
    {
        s_entries.Add(new AuditEntry(accountNumber, type, amount, date, transferId));
    }

    /// <summary>
    /// Возвращает все записи журнала аудита.
    /// </summary>
    /// <returns>Список записей.</returns>
    public static IReadOnlyList<AuditEntry> GetEntries() => s_entries;

    /// <summary>
    /// Формирует текстовый отчёт по всем записям журнала.
    /// </summary>
    /// <returns>Строка со всеми записями аудита.</returns>
    public static string GetReport()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Account\t\tType\t\tAmount\tDate");
        foreach (var e in s_entries)
        {
            sb.AppendLine($"{e.AccountNumber}\t{e.Type}\t{e.Amount}\t{e.Date.ToShortDateString()}");
        }
        return sb.ToString();
    }

    /// <summary>
    /// Очищает журнал аудита.
    /// </summary>
    public static void Clear() => s_entries.Clear();
}
