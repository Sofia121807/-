namespace pr2;

/// <summary>
/// Одна транзакция по счету
/// </summary>
/// <param name="Amount">Сумма операции.</param>
/// <param name="date">Дата совершения операции.</param>
/// <param name="Note">Комментарий к операции.</param>
/// <param name="TransferId">
/// Идентификатор перевода, объединяющий парные операции (списание и зачисление).
/// Для обычных операций равен <c>null</c>.
/// </param>
public record Transactionn(decimal Amount, DateTime date, string Note, Guid? TransferId = null); // record - Состояние объектов этого класса нельзя изменить

// это то же самое 

//internal record Transactionn
//{
//    public decimal Amount { get; }
//    public DateTime Date { get; }
//    public string Note { get; }
//     public Transactionn(decimal Amount, DateTime date, string Note)
//    { 
//        this.Amount = Amount;
//        this.Note = Note;
//        this.Date = Date;
//    }
//}