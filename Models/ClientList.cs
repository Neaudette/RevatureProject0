using System;
using System.Collections.Generic;

namespace Revature_Project_1.Models;

public partial class ClientList
{
    public int ClientId { get; set; }

    public string ClientName { get; set; } = null!;

    public string ClientPassword { get; set; } = null!;

    public bool IsAdministrator { get; set; }

    public virtual ICollection<AccountList> AccountLists { get; set; } = new List<AccountList>();
}
