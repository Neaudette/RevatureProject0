// See https://aka.ms/new-console-template for more information

using Revature_Project_1.Models;
using Revature_Project_1.Handlers;

bool exit = false;

while(!exit)
{
    Console.Clear();
    Console.WriteLine("\n\n\n!!~~~~~~~~~~~~~~~~~~~~ Welcome to NickyBank! ~~~~~~~~~~~~~~~~~~~~!!\n");
    Console.WriteLine("How can we help you today?\nPlease Enter your Selection by Number:\n");
    Console.WriteLine("1. I am a User");
    Console.WriteLine("2. I am an Administrator");
    Console.WriteLine("3. Exit\n");
    int UserInput;
    int transfer;
    
    AccountList ActiveAccount;
    AccountList DestinationAccount;
    ClientList ActiveClient;
    

    try
    {
        UserInput = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));

        switch(UserInput)
        {
            case 1: //TODO: Case 6
            {
                
                string username;
                string password;
                Console.WriteLine("Please Enter your Username");
                username = Console.ReadLine() ?? throw new Exception("Invalid Username!");
                Console.WriteLine("Please Enter your Password");
                password = Console.ReadLine()?? throw new Exception("Invalid Password!");

                ClientHandler handler = new ClientHandler();
                ActiveClient = handler.checkClientLogin(username, password);

                if(ActiveClient != null)
                {
                    Console.WriteLine("Login Successful!");
                    Console.WriteLine("Press Enter to Continue...");
                    Console.ReadLine();
                    bool loggedIn = true;

                    while(loggedIn)
                    {
                        Console.Clear();
                        handler.DisplayClientMenu(); 

                        UserInput = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));

                        switch(UserInput)
                        {
                            case 1: //DONE

                                Console.WriteLine("!!!~~~~~~~~~~~~~~Account Summary~~~~~~~~~~~~~~!!!\n");
                                handler.ListAccounts(ActiveClient.ClientId);
                                Console.WriteLine("Press Enter to Continue...");
                                Console.ReadLine();
                                break;

                            case 2: //DONE

                                Console.WriteLine("Enter the ID of the Account you would like to close");
                                handler.ListAccounts(ActiveClient.ClientId);
                                UserInput = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));

                                ActiveAccount = handler.GetAccountByID(UserInput);

                                Console.WriteLine("Select an Account you would like to transfer funds to");
                                handler.ListAccounts(ActiveClient.ClientId);
                                UserInput = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));

                                DestinationAccount = handler.GetAccountByID(UserInput);

                                transfer = ActiveAccount.AvailableBalance;
                                handler.Transfer(ActiveAccount.AccountId, DestinationAccount.AccountId, transfer);
                                handler.CloseAccount(ActiveAccount.AccountId);
                                Console.ReadLine();

                                break;

                            case 3: //DONE
                                
                                Console.WriteLine("Enter the ID of the Account you would like to Withdraw from\n");
                                handler.ListAccounts(ActiveClient.ClientId);
                                UserInput = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));

                                ActiveAccount = handler.GetAccountByID(UserInput);

                                Console.WriteLine($"Enter the Amount to Withdraw from {ActiveAccount.AccountName}\n");
                                int withdraw = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));
                                handler.Withdraw(ActiveAccount.AccountId, withdraw);
                                Console.WriteLine("Press Enter to Continue...");
                                Console.ReadLine();

                                break;

                            case 4: //DONE

                                Console.WriteLine("Enter the ID of the Account you would like to Deposit to\n");
                                handler.ListAccounts(ActiveClient.ClientId);
                                UserInput = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));

                                ActiveAccount = handler.GetAccountByID(UserInput);

                                Console.WriteLine($"Enter the Amount to Deposit to {ActiveAccount.AccountName}\n");
                                int deposit = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));
                                handler.Deposit(ActiveAccount.AccountId, deposit);
                                Console.WriteLine("Press Enter to Continue...");
                                Console.ReadLine();

                            break;

                            case 5: //DONE

                                Console.WriteLine("Enter the ID of the Account you would like to Transfer from\n");
                                handler.ListAccounts(ActiveClient.ClientId);
                                UserInput = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));

                                ActiveAccount = handler.GetAccountByID(UserInput);

                                Console.WriteLine("Enter the ID of the Account you would like to Transfer to\n");
                                handler.ListAccounts(ActiveClient.ClientId);
                                UserInput = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));

                                DestinationAccount = handler.GetAccountByID(UserInput);

                                Console.WriteLine($"Enter the Amount to Deposit to {ActiveAccount.AccountName}\n");
                                transfer = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));
                                handler.Transfer(ActiveAccount.AccountId, DestinationAccount.AccountId, transfer);
                                Console.WriteLine("Press Enter to Continue...");
                                Console.ReadLine();

                            break;

                            case 6: //TODO: Implement
                                Console.WriteLine("Cheque Book Requested");
                                Console.ReadLine();
                                break;

                            case 7: //DONE  
                                Console.WriteLine("Enter the ID of the Account you would like a summary of");
                                handler.ListAccounts(ActiveClient.ClientId);
                                UserInput = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));

                                ActiveAccount = handler.GetAccountByID(UserInput);
                                Console.WriteLine($"Listing the last 5 transactions for {ActiveAccount.AccountName}:");
                                handler.ListTransactions(handler.GetLast5Transactions(ActiveAccount.AccountId));    

                                Console.WriteLine("\nPress Enter to Continue...");
                                Console.ReadLine();
                                break;

                            case 8: //DONE

                            Console.WriteLine("For security purposes, please enter your password.");
                            string checkPassword = Console.ReadLine() ?? throw new Exception("Invalid Password!");
                            if(checkPassword == password){
                                Console.WriteLine("Please Enter a New Password.");
                                checkPassword = Console.ReadLine() ?? throw new Exception("Invalid Password!");
                                Console.WriteLine("Please re-enter your New Password.");
                                if(checkPassword == (Console.ReadLine() ?? throw new Exception("Invalid Password!")))
                                {
                                    handler.ChangePassword(ActiveClient.ClientId, checkPassword);
                                    Console.ReadLine();
                                    break;
                                }
                            }
                            Console.WriteLine("Passwords do not match. \nAborting password change.");
                            Console.ReadLine();
                            break;

                            case 9: //DONE
                                Console.Clear();
                                while(true)
                                {
                                    Console.WriteLine("Are you sure you wish to logout?");
                                    Console.WriteLine("1. Yes");
                                    Console.WriteLine("2. No");
                                    UserInput = Convert.ToInt32(Console.ReadLine());
                                    if(UserInput == 1)
                                    {
                                        loggedIn = false;
                                        Console.WriteLine("Thank you for Banking with us!");
                                        Console.WriteLine("Press Enter to Exit...");
                                        Console.ReadLine();
                                        break;
                                    }
                                    else if(UserInput == 2)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please Enter a Numeric Input from the Menu.");
                                        Console.ReadLine();
                                        Console.Clear();
                                    }
                                }
                                break;

                            default: //DONE
                                throw new Exception("Please Enter a Numeric Input from the Menu.");
                        }

                    }               

                }
                else //DONE
                {
                    System.Console.WriteLine("Invalid Credentials!");
                    Console.ReadLine();
                }
            }
            break;

            case 2: //TODO: Case 6
            {

                string username;
                string password;
                Console.WriteLine("Please Enter your Username");
                username = Console.ReadLine() ?? throw new Exception("Invalid Username!");
                Console.WriteLine("Please Enter your Password");
                password = Console.ReadLine()?? throw new Exception("Invalid Password!");

                AdminHandler handler = new AdminHandler();
                ActiveClient = handler.CheckAdminCredentials(username, password);

                if(ActiveClient != null)
                {
                    Console.WriteLine("Login Successful!");
                    Console.WriteLine("Press Enter to Continue...");
                    Console.ReadLine();
                    bool loggedIn = true;

                    while(loggedIn){

                        Console.Clear();
                        handler.DisplayAdminMenu(); 

                        UserInput = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));

                        switch(UserInput)
                        {
                            
                            case 1: //DONE
                            {
                                Console.WriteLine("Enter the name of this account");
                                string accountName = Console.ReadLine() ?? throw new Exception("Invalid Input Exception!");

                                Console.WriteLine("Enter the initial balance to deposit into the account");
                                int startingBalance = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));

                                Console.WriteLine("Enter the client that this account belongs to");
                                handler.DisplayClientList(handler.GetClientList());
                                int client = Convert.ToInt32(Console.ReadLine()) - 1;

                                handler.CreateNewAccount(accountName, handler.GetClientList().ElementAt(client).ClientId, startingBalance);
                                break;
                            }

                            case 2: //DONE
                            {
                                Console.WriteLine("Select the client");
                                handler.DisplayClientList(handler.GetClientList());
                                int clientID = handler.GetClientList().ElementAt(Convert.ToInt32(Console.ReadLine()) - 1).ClientId;

                                Console.WriteLine("Select the account to be deleted");
                                handler.DisplayAccountList(handler.GetAccountsByUser(clientID));
                                AccountList account = handler.GetAccountByID(handler.GetAccountsByUser(clientID).ElementAt(Convert.ToInt32(Console.ReadLine()) - 1).AccountId);
                                

                                Console.WriteLine($"Are you sure you wish to delete this account?\n1. Yes\n2. No");
                                int confirm = Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));
                                if(confirm == 1)
                                {
                                    handler.DeleteAccount(account.AccountId);
                                }
                                else
                                {
                                    Console.WriteLine("Account Deletion Aborted");
                                    Console.WriteLine("Press Enter to Continue...");
                                    Console.ReadLine();
                                }
                                break;
                            }
                            
                            case 3: //DONE
                            {
                                Console.WriteLine("Select the client");
                                handler.DisplayClientList(handler.GetClientList());
                                ClientList client = handler.GetClientList().ElementAt(Convert.ToInt32(Console.ReadLine()) - 1);

                                Console.WriteLine("Select the account to be edited");
                                handler.DisplayAccountList(handler.GetAccountsByUser(client.ClientId));
                                AccountList account = handler.GetAccountByID(handler.GetAccountsByUser(client.ClientId).ElementAt(Convert.ToInt32(Console.ReadLine()) - 1).AccountId);
                                
                                Console.WriteLine($"{account.AccountName} - {account.AvailableBalance}, Owned by {client.ClientName}");
                                Console.WriteLine("1. Edit Name");
                                Console.WriteLine("2. Edit Balance");
                                Console.WriteLine("3. Edit Status");
                                Console.WriteLine("4. Exit");
                                UserInput = Convert.ToInt32(Console.ReadLine());

                                switch(UserInput)
                                {
                                    case 1:

                                        Console.WriteLine("Enter a new name for the account");
                                        handler.RenameAccount(account.AccountId, Console.ReadLine() ?? throw new Exception("Invalid Input Exception!"));
                                        Console.WriteLine("Press Enter to Continue...");
                                        Console.ReadLine();
                                        break;
                                    
                                    case 2:

                                        Console.WriteLine("Enter this account's balance");
                                        handler.ChangeAccountBalance(account.AccountId, Convert.ToInt32(Console.ReadLine() ?? throw new Exception("Invalid Input Exception!")));
                                        Console.WriteLine("Press Enter to Continue...");
                                        Console.ReadLine();
                                        break;
                                    
                                    case 3:

                                        handler.OpenCloseAccount(account.AccountId);
                                        break;

                                    case 4:
                                        Console.WriteLine("Exiting Account Editor");
                                        Console.WriteLine("Press Enter to Continue...");
                                        Console.ReadLine();
                                        break;

                                }

                                
                                break;
                            }

                            case 4: //DONE
                            {
                                Console.WriteLine("!!!~~~~~~~~~~~~~~Account Summary~~~~~~~~~~~~~~!!!");

                                ClientHandler clientHandler= new ClientHandler();

                                foreach(ClientList client in handler.GetClientList())
                                {
                                    Console.WriteLine($"\n{client.ClientName}'s Accounts:");
                                    clientHandler.ListAccounts(client.ClientId);
                                }

                                Console.WriteLine("\nPress Enter to Continue...");
                                Console.ReadLine();
                                break;
                            }

                            case 5: //DONE
                            {
                                Console.WriteLine("For security purposes, please enter your password.");
                                string checkPassword = Console.ReadLine() ?? throw new Exception("Invalid Password!");
                                if(checkPassword == password){
                                    Console.WriteLine("Select the client");
                                    handler.DisplayClientList(handler.GetClientList());
                                    int clientID = handler.GetClientList().ElementAt(Convert.ToInt32(Console.ReadLine()) - 1).ClientId;

                                    Console.WriteLine("Please Enter a New Password.");
                                    checkPassword = Console.ReadLine() ?? throw new Exception("Invalid Password!");
                                    Console.WriteLine("Please re-enter your New Password.");
                                    if(checkPassword == (Console.ReadLine() ?? throw new Exception("Invalid Password!")))
                                    {
                                        new ClientHandler().ChangePassword(clientID, checkPassword);
                                        Console.ReadLine();
                                        break;
                                    }
                            }
                            Console.WriteLine("Passwords do not match. \nAborting password change.");
                            Console.ReadLine();
                            break;

                            }
                            
                            case 6: //TODO: Implement
                                Console.WriteLine("Requests Validated Successfully.");
                                break;
                            
                            case 7: //DONE

                                Console.Clear();
                                while(true)
                                {
                                    Console.WriteLine("Are you sure you wish to logout?");
                                    Console.WriteLine("1. Yes");
                                    Console.WriteLine("2. No");
                                    UserInput = Convert.ToInt32(Console.ReadLine());
                                    if(UserInput == 1)
                                    {
                                        loggedIn = false;
                                        Console.WriteLine("Thank you for Banking with us!");
                                        Console.WriteLine("Press Enter to Exit...");
                                        Console.ReadLine();
                                        break;
                                    }
                                    else if(UserInput == 2)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please Enter a Numeric Input from the Menu.");
                                        Console.ReadLine();
                                        Console.Clear();
                                    }
                                }
                                break;

                            default: //DONE
                                throw new Exception("Please Enter a Numeric Input from the Menu.");


                        }

                    }
                }
                else //DONE
                {
                    System.Console.WriteLine("Invalid Credentials!");
                    Console.ReadLine();
                }

            }
            break;

            case 3: //DONE

                Console.Clear();
                while(true)
                {
                    Console.WriteLine("Are you sure you wish to exit?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");
                    UserInput = Convert.ToInt32(Console.ReadLine());
                    if(UserInput == 1)
                    {
                        exit = true;
                        Console.WriteLine("Thank you for Banking with us!");
                        Console.ReadLine();
                        break;
                    }
                    else if(UserInput == 2)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please Enter a Numeric Input from the Menu.");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                break;

            default:
                throw new Exception("Please Enter a Numeric Input from the Menu.");
        }

    }
    catch(Exception ex)
    {
        Console.WriteLine($"{ex.Message}\nPress Enter to Continue");
        Console.ReadLine();
    }

}
