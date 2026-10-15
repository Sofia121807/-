using System;
using System.Collections.Generic;
using System.Text;

namespace pr2;

/// <summary>
/// Статус клиента банка
/// </summary>
public enum AccountStatus
{
    /// <summary>
    /// Regular — комиссия за обслуживание 50 руб/мес
    /// </summary>
    Regular,
    /// <summary>
    /// Premium — комиссия 0 руб/мес, но 1% на остаток свыше 10 000
    /// </summary>
    Premium,
    /// <summary>
    /// VIP — комиссия 0, 3% на остаток свыше 10 000, кэшбэк 5% на все списания
    /// </summary>
    VIP
}
