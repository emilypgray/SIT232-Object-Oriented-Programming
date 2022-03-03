using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _3._4DT
{
    class Program
    {
        // Create enum for main menu options
        enum MenuOption 
        {
            Add = 1,
            List,
            Sort,
            Account,
            Exit,
        }

        // Create enum for account menu options
        enum AccountOption
        {
            Deposit = 1,
            Withdraw,
            Print,
            Exit,
        }
        static void Main(string[] args)
        {
            // New empty list to hold accounts as they are added
            List<Account> accountList = new List<Account>();
            int accountSelect;
            int menuSelect;


            // Call ReadUserOption to prompt user to input menu selection
            menuSelect = ReadUserOption();

            // Use do while loop to continue calling the ReadUserOption method until
            // the quit option is selected
            do
            {
                // Use switch statement to perform operations based on user menu
                // selection
                switch (menuSelect)
                {
                    case (int)MenuOption.Add:
                        Console.WriteLine();
                        Console.WriteLine("You have selected to add an account.");
                        Console.WriteLine();
                                             
                        bool isNameValid = false;
                        bool isBalanceValid = false;

                        do
                        {
                            Console.WriteLine("Please enter the account name: ");
                            string name = Console.ReadLine();

                            // Check that input is not null or empty
                            if (!String.IsNullOrEmpty(name))
                            {
                                isNameValid = true;
                                do
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Please enter the opening balance: ");

                                    // Check the input can be converted toi decimal type
                                    try
                                    {
                                        decimal balance = Convert.ToDecimal(Console.ReadLine());
                                        isBalanceValid = true;
                                        // Create a new object of account class with inputs
                                        Account accountInstance = new Account(name, balance);
                                        // Add object to account list
                                        accountList.Add(accountInstance);
                                        Console.WriteLine();
                                        Console.WriteLine("Account has been added to the database.");
                                    }
                                    catch
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Invalid input.");
                                    }
                                } while (!isBalanceValid);                               
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Name cannot be empty.");
                                Console.WriteLine();
                            }
                        } while (!isNameValid);                         
                        
                        menuSelect = ReadUserOption();
                        break;

                    case (int)MenuOption.List:
                        Console.WriteLine();                       
                        Console.WriteLine("You have selected to list the accounts.");
                        Console.WriteLine();
                        // Check account list is not zero. If it isn't display account, otherwise
                        // show error and go back to menu
                        if (accountList.Count != 0)
                        {
                            Console.WriteLine("The accounts are: ");
                            Console.WriteLine();
                            Console.WriteLine("-------------------------------------------");
                            Console.WriteLine("{0,-25}|\t{1}", "Name", "Balance");
                            Console.WriteLine("-------------------------------------------");
                            // Use for loop to iterate through each element in the account list
                            // and output account info to the console
                            for (int i = 0; i < accountList.Count; i++)
                                Console.WriteLine("{0,-25}|\t{1,5}", accountList[i].Name, accountList[i].Balance);
                            menuSelect = ReadUserOption();
                        }
                        else
                        {
                            Console.WriteLine("There are no accounts to display.Please add an account first.");
                            menuSelect = ReadUserOption();
                        }
                        break;
                    case (int)MenuOption.Sort:
                        Console.WriteLine();
                        Console.WriteLine("You have selected to view the sorted accounts.");
                        Console.WriteLine();
                        // Check account list is not zero. If it isn't display account, otherwise
                        // show error and go back to menu
                        if (accountList.Count != 0)
                        {
                            Console.WriteLine("The Accounts Sorted by Ascending Balance are: ");
                            Console.WriteLine();
                            // Convert account list to array type and call the AccountSorter
                            // method using the array as input
                            Account[] accountArray = accountList.ToArray();
                            AccountsSorter.Sort(accountArray, accountArray.Length);
                            menuSelect = ReadUserOption();
                        }
                        else
                        {
                            Console.WriteLine("There are no accounts to display.Please add an account first.");
                            menuSelect = ReadUserOption();
                        }
                        
                        break;

                    case (int)MenuOption.Account:
                        Console.WriteLine();
                        Console.WriteLine("You have selected to modify an existing account.");
                        // Search for account
                        int accountIndex = AccountIndex();
                        // If account is found, display menu, otherwise go back to main menu
                        if (accountIndex != -1)
                        {
                            Console.WriteLine();
                            Console.WriteLine("What would you like to do: ");
                            
                            // Display account options
                            accountSelect = ReadAccountOption();
                            do
                            {
                                switch (accountSelect)
                                {
                                    case (int)AccountOption.Deposit:
                                        Console.WriteLine();
                                        Console.WriteLine("You have selected to deposit money into this account.");
                                        // Call deposit method from account class
                                        accountList[accountIndex].Deposit();
                                        accountSelect = ReadAccountOption();
                                        break;
                                    case (int)AccountOption.Withdraw:
                                        Console.WriteLine();
                                        Console.WriteLine("You have selected to withdraw money from this account.");
                                        // Call withdraw method from account class
                                        accountList[accountIndex].Withdraw();
                                        accountSelect = ReadAccountOption();
                                        break;
                                    case (int)AccountOption.Print:
                                        // Call print method from account class
                                        accountList[accountIndex].Print();
                                        accountSelect = ReadAccountOption();
                                        break;
                                    case (int)AccountOption.Exit:                                                                             
                                        break;
                                }
                            } while (accountSelect != 4);
                        }
                        else
                        {
                            menuSelect = ReadUserOption();
                            break;
                        }
                        Console.WriteLine("Returning to main menu...");
                        menuSelect = ReadUserOption();
                        break;
                    case (int)MenuOption.Exit:
                        break;
                }
            } while (menuSelect != (int)MenuOption.Exit);
            Console.WriteLine("Exiting the program...");



            // Declare method to locate index of particular account in account list
            int AccountIndex()
            {
                bool foundName = false;

                // If the account list is not empty, search for account by name. Otherwise,
                // return an error message
                if (accountList.Count != 0)
                {
                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please Enter Account Name: ");
                        string accountName = Console.ReadLine();

                        // Use for loop to iterate through the account list and locate
                        // index of account. Return index
                        for (int i = 0; i < accountList.Count; i++)
                        {
                            if (accountList[i].Name == accountName)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Account found.");
                                foundName = true;
                                return i;
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Account not found. Try again.");
                    } while (!foundName);
                    return -1;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("There are no accounts to modify. Please add an account first.");
                    return -1;
                }                          
            }
            



            int ReadUserOption()
            {
                bool isValidInput = false;

                do
                {
                    // Show the menu
                    Console.WriteLine();
                    Console.WriteLine("Please select an option (1 - 5) from the following: ");
                    Console.WriteLine();
                    Console.WriteLine("1. Add Account");
                    Console.WriteLine("2. View Accounts");
                    Console.WriteLine("3. View Sorted Accounts ");
                    Console.WriteLine("4. Modify an Existing Account");
                    Console.WriteLine("5. Exit");
                    Console.WriteLine();

                    // Use TryParse to test if input can be converted to an integer value
                    isValidInput = Int32.TryParse(Console.ReadLine(), out int menuSelection);

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
                } while (!isValidInput);
                return 0;
            }



            int ReadAccountOption()
            {
                bool isValidInput = false;

                do
                {
                    // Show the menu
                    Console.WriteLine();
                    Console.WriteLine("Please select an option (1 - 4) from the following: ");
                    Console.WriteLine();
                    Console.WriteLine("1. Make a Deposit");
                    Console.WriteLine("2. Make a Withdrawal");
                    Console.WriteLine("3. Print Account Information ");
                    Console.WriteLine("4. Return to Main Menu");
                    Console.WriteLine();

                    // Use TryParse to test if input can be converted to an integer value
                    isValidInput = Int32.TryParse(Console.ReadLine(), out int menuSelection);

                    // If input can be converted to integer
                    if (isValidInput)
                    {
                        // If input in correct range
                        if (menuSelection > 0 && menuSelection < 5)
                            // Return input minus 1 to main program
                            return menuSelection;
                        else
                            isValidInput = false;
                    }
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please try again.");
                } while (!isValidInput);
                return 0;
            }




        }

    }
}
