using System;
using System.Collections.Generic;
using System.Text;

namespace pr2;

/// <summary>
/// Сервис перевода денег между счетами.
/// </summary>
public static class Transfers
{
    /// <summary>
    /// Переводит деньги со счёта <paramref name="from"/> на счёт <paramref name="to"/>.
    /// </summary>
    /// <param name="from">Счёт-источник.</param>
    /// <param name="to">Счёт-получатель.</param>
    /// <param name="amount">Сумма перевода. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentNullException">
    /// Бросается, если <paramref name="from"/> или <paramref name="to"/> равны <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Бросается, если <paramref name="amount"/> меньше или равен нулю.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Бросается, если списание со счёта-источника невозможно.
    /// </exception>
    public static void Transfer(BankAccount from, BankAccount to, decimal amount, DateTime date, string note)
    {
        if (from is null) throw new ArgumentNullException(nameof(from));
        if (to is null) throw new ArgumentNullException(nameof(to));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        var transferId = Guid.NewGuid();

        from.MakeWithdrawal(amount, date, $"Transfer out to {to.Number}: {note}", transferId);

        try
        {
            to.MakeDeposit(amount, date, $"Transfer in from {from.Number}: {note}", transferId);
        }
        catch
        {
            from.MakeDeposit(amount, date, $"Rollback of failed transfer to {to.Number}", transferId);
            throw;
        }
    }
}
