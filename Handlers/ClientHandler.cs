using Revature_Project_1.Models;

namespace Revature_Project_1.Handlers;

public class ClientHandler
{

    MasterBankingDbContext db = new MasterBankingDbContext();

    public ClientList checkClientLogin(string username, string password)
    {
        var client = db.ClientLists.SingleOrDefault(a => a.ClientName == username && a.ClientPassword == password);

        return client;
    }

    public void DisplayClientMenu()
    {

        Console.WriteLine("How can we help you today?\n");
        Console.WriteLine("1. Account Summary");
        Console.WriteLine("2. Close an Account");
        Console.WriteLine("3. Withdraw from Account");
        Console.WriteLine("4. Deposit to Account");
        Console.WriteLine("5. Transfer to Account");
        Console.WriteLine("6. Request a Cheque Book");
        Console.WriteLine("7. List Last 5 Transactions");
        Console.WriteLine("8. Change Password");
        Console.WriteLine("9. Exit");

    }

    public void Withdraw(int accountID, int amount)
    {

        //withdraw logic, LINQ query to update the table 

        if(amount <= 0)
        {
            throw new Exception($"Withdrawal amount must be greater than zero.");
        }

        var account = db.AccountLists.SingleOrDefault(a => a.AccountId == accountID && a.IsOpen == true) ?? throw new Exception("No Open Account Matches the Given Criteria");

        if (account.AvailableBalance < amount) 
        {
            throw new InvalidOperationException($"Insufficient Balance. Account has {account.AvailableBalance} available.");
        }

        account.AvailableBalance -= amount;

        db.Update(account);
        db.Transactions.Add(new Transaction{
            TransactionId = new Random().Next(1000000, 9999999),
            Account1 = account.AccountId,
            TransactionType = "Withdrawal",
            Amount = amount

        });
        db.SaveChanges();

        Console.WriteLine($"Operation Successful: Withdrawn {amount} from {account.AccountName}");
    }

    public void Deposit(int accountID, int amount)
    {
        //Deposit logic, LINQ query to update the table

        if(amount <= 0)
        {
            throw new Exception($"Deposit amount must be greater than zero.");
        }

        var account = db.AccountLists.SingleOrDefault(a => a.AccountId == accountID && a.IsOpen == true) ?? throw new Exception("No Open Account Matches the Given Criteria");

        account.AvailableBalance += amount;

        db.Update(account);
        db.Transactions.Add(new Transaction{
            TransactionId = new Random().Next(1000000, 9999999),
            Account1 = account.AccountId,
            TransactionType = "Deposit",
            Amount = amount

        });
        db.SaveChanges();

        Console.WriteLine($"Operation Successful: Deposited {amount} to {account.AccountName}");
    }

    public void Transfer(int accountID, int accountID2, int amount)
    {
        //Transfer logic, LINQ query to update the table

        if (accountID == accountID2)
        {
            Console.WriteLine("Cannot transfer funds to the same account.");
            Console.WriteLine("Press Enter to Continue...");
            return;
        }

        Withdraw(accountID,amount);
        Deposit(accountID2, amount);

        Console.WriteLine("Transfer successful.");
    }

    public void CloseAccount(int accountID)
    {
        //Deposit logic, LINQ query to update the table

        var account = db.AccountLists.SingleOrDefault(a => a.AccountId == accountID && a.IsOpen == true) ?? throw new Exception("No Open Account Matches the Given Criteria");

        account.IsOpen = false;

        db.Update(account);
        db.SaveChanges();

        Console.WriteLine($"Operation Successful: Account {accountID} is now closed.");
        Console.WriteLine("Press Enter to Continue...");
    }

    public void ListAccounts(int clientID)
    {
        var clientAccounts = db.AccountLists.Where(a => a.ClientId == clientID).ToList();

        foreach(AccountList account in clientAccounts)
        {
            Console.WriteLine($"{account.AccountId}. {account.AccountName} - Balance: {account.AvailableBalance}");
        }
    }

    public void ChangePassword(int clientID, string NewPassword)
    {
        var client = db.ClientLists.Where(a => a.ClientId == clientID).Single();

        client.ClientPassword = NewPassword;

        db.Update(client);
        db.SaveChanges();

        Console.WriteLine("Password Changed Succesfully!");
        Console.WriteLine("Press Enter to Continue...");
    }

    public AccountList GetAccountByID(int accountID)
    {
        var account = db.AccountLists.Where(a => a.AccountId == accountID && a.IsOpen == true).SingleOrDefault() ?? throw new Exception("No Open Account Matches the Given Criteria");

        return account;

    }

    public List<Transaction> GetLast5Transactions(int accountID)
    {
        return db.Transactions.Where(a => a.Account1 == accountID || a.Account2 == accountID).Take(5).ToList();
    }

    public void ListTransactions(List<Transaction> transList)
    {
        for(int i = 0; i < transList.Count; i++)
        {
            Console.WriteLine($"{i+1}. Account {transList[i].Account1}, {transList[i].TransactionType}: {transList[i].Amount}");
        }
    }

}

