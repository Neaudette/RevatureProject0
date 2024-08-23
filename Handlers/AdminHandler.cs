using Azure.Core.Pipeline;
using Revature_Project_1.Models;

namespace Revature_Project_1.Handlers;

public class AdminHandler
{

    MasterBankingDbContext db = new MasterBankingDbContext();

    public ClientList CheckAdminCredentials(string username, string password)
    {
        var admin = db.ClientLists.SingleOrDefault(a => a.ClientName == username && a.ClientPassword == password && a.IsAdministrator == true);

        return admin;
    }

    public void DisplayAdminMenu()
    {

        Console.WriteLine("1. Create New Account");
        Console.WriteLine("2. Delete Account");
        Console.WriteLine("3. Edit Account Details");
        Console.WriteLine("4. Display Summary");
        Console.WriteLine("5. Reset Customer Password");
        Console.WriteLine("6. Approve Cheque book request");
        Console.WriteLine("7. Exit");

    }

    public  void CreateNewAccount(string name, int clientID, int initialBalance)
    {
        //we can validate the data here

        if(initialBalance < 0)
        {
            Console.WriteLine("Deposit amount CANNOT be negative!");
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();
            return;
        }

        AccountList newAccount = new AccountList
        {
            AccountId = new Random().Next(10000, 99999),
            AccountName = name,
            AvailableBalance = initialBalance,
            IsOpen = true,
            ClientId = clientID
        };

        db.AccountLists.Add(newAccount);
        db.SaveChanges();

        Console.WriteLine("Account Created Successfully!");
        Console.WriteLine("Press Enter to Continue...");
        Console.ReadLine();
    }

    public void DeleteAccount(int accountID)
    {
        //we can validate the data here
        var account = db.AccountLists.Find(accountID) ?? throw new Exception("No Open Account Matches the Given Criteria");
        db.AccountLists.Remove(account);
        db.SaveChanges();

        Console.WriteLine("Account Deleted Successfully!");
        Console.WriteLine("Press Enter to Continue...");
        Console.ReadLine();
    }

    public void RenameAccount(int accountID, string name)
    {
        //we can validate the data here

        AccountList account = db.AccountLists.Where(a => a.AccountId == accountID).Single() ?? throw new Exception("No Account Matches the Given Criteria");

        Console.WriteLine($"Account {account.AccountName} successfully renamed to {name}");

        account.AccountName = name;
        
        db.AccountLists.Update(account);
        db.SaveChanges();
    }

    public void ChangeAccountBalance(int accountID, int amount)
    {
        //we can validate the data here

        AccountList account = db.AccountLists.Where(a => a.AccountId == accountID).Single() ?? throw new Exception("No Account Matches the Given Criteria");

        Console.WriteLine($"Account {account.AccountName} successfully changed to {amount}");

        account.AvailableBalance = amount;
        
        db.AccountLists.Update(account);
        db.SaveChanges();
    }

    public void OpenCloseAccount(int accountID)
    {
        //we can validate the data here

        AccountList account = db.AccountLists.Where(a => a.AccountId == accountID).Single() ?? throw new Exception("No Account Matches the Given Criteria");

        account.IsOpen = !account.IsOpen;
        
        if(account.IsOpen)
        {
            Console.WriteLine("Account Opened Successfully!");
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Account Closed Successfully!");
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();
        }

        db.AccountLists.Update(account);
        db.SaveChanges();
    }

    public AccountList GetAccountByID(int accountID)
    {
        return db.AccountLists.Where(a => a.AccountId == accountID && a.IsOpen == true).SingleOrDefault() ?? throw new Exception("No Open Account Matches the Given Criteria");
    }

    public List<AccountList> GetAccountsByUser(int clientID)
    {
        return db.AccountLists.Where(a => a.ClientId == clientID && a.IsOpen == true).ToList() ?? throw new Exception("No Open Account Matches the Given Criteria");
    }

    public void DisplayAccountList(List<AccountList> accounts)
    {
        for(int i = 0; i < accounts.Count(); i++)
        {
            Console.WriteLine($"{i+1}. {accounts[i].AccountName}");
        }
    }

    public List<ClientList> GetClientList()
    {
        return db.ClientLists.ToList();
    }

    public void DisplayClientList(List<ClientList> clients)
    {
        for(int i = 0; i < clients.Count(); i++)
        {
            Console.WriteLine($"{i+1}. {clients[i].ClientName}");
        }
    }

}