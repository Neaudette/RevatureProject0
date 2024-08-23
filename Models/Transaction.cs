using System;
using System.Collections.Generic;

namespace Revature_Project_1.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int Account1 { get; set; }

    public int? Account2 { get; set; }

    public string TransactionType { get; set; } = null!;

    public int Amount { get; set; }

    public virtual AccountList Account1Navigation { get; set; } = null!;

    public virtual AccountList? Account2Navigation { get; set; }
}
