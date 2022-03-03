using System;
using System.Collections.Generic;


namespace _6._2___Multiple_Bank_Accounts
{
    class BankSystem
    {
        // Set up enum for first menu options
        enum FirstMenu
        {
            ListAccounts = 1,
            EnterAccount,
            AddAccount,
            ListTransactions,
            Quit
        }

        // Set up enum for menu options
        enum MenuOption
        {
            Withdraw = 1,
            Deposit,
            Transfer,
            Print,
            Quit
        }


        enum RollbackOption
        {
            Rollback = 1,
            Quit
        }


        // write method to ask for account name and search in bank object for it
        private static Account FindAccount(Bank bank, int isTransfer)
        {
            Console.Write($"Enter {(isTransfer == 0 ? "Account Name: " : "Name of Account to Transfer to: ")}");

            string name = Console.ReadLine();

            // call bank object's GetAccount method
            return bank.GetAccount(name);
        }



        static void Main(string[] args)
        {
            // declare new bank object
            Bank bank = new Bank();

            // Call the first menu method
            int mainMenuOption = ReadMainMenu();
            do
            {
                // Use switch to carry out menu options
                switch (mainMenuOption)
                {
                    case (int)FirstMenu.ListAccounts:

                        // if bank object is not empty
                        if (bank.BankAccount.Count != 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("You have selected to list the accounts:");
                            Console.WriteLine();

                            Console.WriteLine("-------------------------------------------");
                            Console.WriteLine("{0,-25}|\t{1}", "Name", "Balance");
                            Console.WriteLine("-------------------------------------------");

                            // Use foreach to list each account's details
                            foreach (Account item in bank.BankAccount)
                            {
                                Console.WriteLine("{0,-25}|\t{1:c}", item.Name, item.Balance);
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("There are no accounts to list. Please add an account first.");
                        }

                        Console.ReadLine();
                        Console.Clear();

                        // Call main menu method again
                        mainMenuOption = ReadMainMenu();
                        break;

                    case (int)FirstMenu.EnterAccount:

                        if (bank.BankAccount.Count != 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("You have selected to transact on an existing account.");
                            Console.WriteLine();

                            // create new object from FindAccount
                            Account account = FindAccount(bank, 0);

                            // if account was not found
                            if (account != null)
                            {
                                Console.ReadLine();
                                Console.Clear();

                                int menuSelect = ReadUserOption();
                                do
                                {
                                    switch (menuSelect)
                                    {
                                        case (int)MenuOption.Withdraw:

                                            Console.WriteLine();
                                            Console.WriteLine("You have selected Withdraw.");
                                            Console.WriteLine();

                                            Console.Write("Enter amount you wish to Withdraw: ");

                                            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                                            {
                                                // create new WithdrawTransaction object
                                                Transaction withdraw = new WithdrawTransaction(account, amount);

                                                // call bank object's execute method
                                                bank.ExecuteTransaction(withdraw);
                                                withdraw.Print();
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR: Invalid Input. Unable to Process Transaction.");
                                            }

                                            Console.ReadLine();
                                            Console.Clear();

                                            menuSelect = ReadUserOption();
                                            break;

                                        case (int)MenuOption.Deposit:

                                            Console.WriteLine();
                                            Console.WriteLine("You have selected Deposit.");
                                            Console.WriteLine();

                                            Console.Write("Enter amount you wish to Deposit: ");

                                            if (decimal.TryParse(Console.ReadLine(), out amount))
                                            {
                                                // create new DepositTransaction object
                                                Transaction deposit = new DepositTransaction(account, amount);

                                                // call bank object's execute method
                                                bank.ExecuteTransaction(deposit);
                                                deposit.Print();
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR: Invalid Input. Unable to Process Transaction.");
                                            }

                                            Console.ReadLine();
                                            Console.Clear();

                                            menuSelect = ReadUserOption();
                                            break;

                                        case (int)MenuOption.Transfer:

                                            Console.WriteLine();
                                            Console.WriteLine("You have selected Transfer.");
                                            Console.WriteLine();

                                            Account accountTo = FindAccount(bank, 1);

                                            if (accountTo != null)
                                            {
                                                if (accountTo != account)
                                                {
                                                    Console.WriteLine();
                                                    Console.Write("Enter amount you wish to Transfer: ");

                                                    if (decimal.TryParse(Console.ReadLine(), out amount))
                                                    {
                                                        // create new TransferTransaction object
                                                        Transaction transfer = new TransferTransaction(account, accountTo, amount);

                                                        // call bank object's execute method
                                                        bank.ExecuteTransaction(transfer);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("ERROR: Invalid Input. Unable to Process Transaction.");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("ERROR: Unable to Transfer to the Source Account.");
                                                }
                                            }                                      

                                            Console.ReadLine();
                                            Console.Clear();

                                            menuSelect = ReadUserOption();
                                            break;

                                        case (int)MenuOption.Print:

                                            Console.WriteLine();
                                            Console.WriteLine("You have selected print.");
                                            Console.WriteLine();

                                            // Print account information
                                            Print(account);

                                            Console.ReadLine();
                                            Console.Clear();

                                            menuSelect = ReadUserOption();
                                            break;

                                        case (int)MenuOption.Quit:
                                            break;
                                    }
                                } while (menuSelect != (int)MenuOption.Quit);

                                Console.WriteLine("Returning to Main Menu...");
                            }
                            else
                            {
                                Console.ReadLine();
                                Console.Clear();
                                mainMenuOption = ReadMainMenu();
                                break;
                            }
                        }
                        else
                        {
                            // else output error message
                            Console.WriteLine();
                            Console.WriteLine("There are no accounts to transact on. Please add an account first.");
                        }

                        Console.ReadLine();
                        Console.Clear();

                        mainMenuOption = ReadMainMenu();
                        break;

                    case (int)FirstMenu.AddAccount:

                        Console.WriteLine();
                        Console.WriteLine("You have selected to add an account.");
                        Console.WriteLine();

                        Console.Write("Enter Account Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter Account Opening Balance: ");

                        if (decimal.TryParse(Console.ReadLine(), out decimal balance))
                        {
                            if (balance >= 0)
                            {
                                // create new account object from input
                                Account newAccount = new Account(name, balance);
                                // add account to bank
                                bank.AddAccount(newAccount);
                                Console.WriteLine();
                                Console.WriteLine("Account Added Successfully.");
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("ERROR: Invalid Input. Opening Balance Cannot Be Negative.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Invalid Input. Unable to Add Account.");
                        }

                        Console.ReadLine();
                        Console.Clear();

                        mainMenuOption = ReadMainMenu();
                        break;

                    case (int)FirstMenu.ListTransactions:

                        try
                        {
                            bank.PrintTransactionHistroy();
                            int rollbackOption = ReadRollBackOption();

                            switch (rollbackOption)
                            {
                                case (int)RollbackOption.Rollback:
                                    List<Transaction> transactionList = bank.TransactionList;

                                    Console.WriteLine();
                                    Console.WriteLine("You Have Selected to Reverse a Transaction");
                                    Console.Write("Enter the Transaction Number to Reverse: ");

                                    bool isValidInput;
                                    try
                                    {
                                        isValidInput = int.TryParse(Console.ReadLine(), out int transactionSelection);

                                        if (transactionSelection >= 0 && transactionSelection < transactionList.Count)
                                        {
                                            Transaction transactionToRollback = transactionList[transactionSelection];
                                            DoRollBack(bank, transactionToRollback);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid Number Selected.");
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Invalid Input");
                                    }

                                    break;

                                case (int)RollbackOption.Quit:

                                    Console.ReadLine();
                                    Console.Clear();

                                    mainMenuOption = ReadMainMenu();
                                    break;
                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine();
                            Console.WriteLine(e.Message);
                        }

                        Console.ReadLine();
                        Console.Clear();

                        mainMenuOption = ReadMainMenu();
                        break;

                    case (int)FirstMenu.Quit:
                        break;
                }
            } while (mainMenuOption != (int)FirstMenu.Quit);




            static void DoRollBack(Bank bank, Transaction transaction)
            {
                bank.RollbackTransaction(transaction);          
            }


            static void Print(Account account)
            {
                // Print account information
                Console.WriteLine("Account Name: " + account.Name);
                Console.WriteLine("Account Balance: {0:c}", account.Balance);
            }



            static int ReadMainMenu()
            {
                bool isValidInput = false;

                do
                {
                    // display menu
                    Console.WriteLine();
                    Console.WriteLine("Please select an option from the following (1 - 5) from the following: ");
                    Console.WriteLine();
                    Console.WriteLine("1. List Accounts");
                    Console.WriteLine("2. Transact on an Account");
                    Console.WriteLine("3. Add an Account");
                    Console.WriteLine("4. List Transactions");
                    Console.WriteLine("5. Exit");
                    Console.WriteLine();

                    // Use TryParse to test if input can be converted to an integer value
                    isValidInput = int.TryParse(Console.ReadLine(), out int menuSelection);

                    // If input can be converted to integer
                    if (isValidInput)
                    {
                        // If input in correct range
                        if (menuSelection > 0 && menuSelection < 6)
                            // Return input minus 1 to main program
                            return menuSelection;
                        else
                            isValidInput = false;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please try again.");
                    
                    Console.ReadLine();
                    
                    Console.Clear();

                } while (!isValidInput);
                return 0;
            }



            static int ReadUserOption()
            {               
                bool isValidInput = false;

                do
                {
                    // Show the menu
                    Console.WriteLine();
                    Console.WriteLine("Please select an option (1 - 4) from the following: ");
                    Console.WriteLine();
                    Console.WriteLine("1. Withdraw");
                    Console.WriteLine("2. Deposit");
                    Console.WriteLine("3. Transfer to Another Account");
                    Console.WriteLine("4. Print");
                    Console.WriteLine("5. Exit to Previous Menu");
                    Console.WriteLine();

                    // Use TryParse to test if input can be converted to an integer value
                    isValidInput = int.TryParse(Console.ReadLine(), out int menuSelection);

                    // If input can be converted to integer
                    if (isValidInput)
                    {
                        // If input in correct range
                        if (menuSelection > 0 && menuSelection < 6)
                            // Return input minus 1 to main program
                            return menuSelection;
                        else
                            isValidInput = false;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please try again.");

                    Console.ReadLine();
                    Console.Clear();

                } while (!isValidInput);
                return 0;
            }


            static int ReadRollBackOption()
            {
                bool isValidInput = false;

                do
                {
                    // Show the menu
                    Console.WriteLine();
                    Console.WriteLine("Please select an option (1 - 2) from the following: ");
                    Console.WriteLine();
                    Console.WriteLine("1. Rollback a Transaction");
                    Console.WriteLine("2. Exit to Previous Menu");
                    Console.WriteLine();

                    // Use TryParse to test if input can be converted to an integer value
                    isValidInput = int.TryParse(Console.ReadLine(), out int menuSelection);

                    // If input can be converted to integer
                    if (isValidInput)
                    {
                        // If input in correct range
                        if (menuSelection > 0 && menuSelection < 3)
                            // Return input minus 1 to main program
                            return menuSelection;
                        else
                            isValidInput = false;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please try again.");
                    
                    Console.ReadLine();
                    Console.Clear();
                } while (!isValidInput);
                return 0;
            }
        }
    }
}
