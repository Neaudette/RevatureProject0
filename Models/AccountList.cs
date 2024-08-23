using System;
using System.Collections.Generic;

namespace Revature_Project_1.Models;

public partial class AccountList
{
    public int AccountId { get; set; }

    public string AccountName { get; set; } = null!;

    public int AvailableBalance { get; set; }

    public bool IsOpen { get; set; }

    public int ClientId { get; set; }

    public virtual ClientList Client { get; set; } = null!;

    public virtual ICollection<Transaction> TransactionAccount1Navigations { get; set; } = new List<Transaction>();

    public virtual ICollection<Transaction> TransactionAccount2Navigations { get; set; } = new List<Transaction>();
}
