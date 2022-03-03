using System;
using System.Collections.Generic;
using System.Linq;

namespace _6._2___Multiple_Bank_Accounts
{
    enum InnerMenu
    {
        Rollback = 1,
        MainMenu
    }
    class Bank
    {
        // declare pirvate list of accounts
        private List<Account> _accounts;
        private List<Transaction> _transactions;

        // constructor to create empty list
        public Bank()
        {
            _accounts = new List<Account> { };
            _transactions = new List<Transaction> { };
        }

        // public method to return list of accounts
        public List<Account> BankAccount { get { return _accounts; } }

        public List<Transaction> TransactionList { get { return _transactions; } }

        //public List<Account> Bank { get { return _accounts; } }


        // add account to list
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public Account GetAccount(String name)
        {
            // use foreach to loop through list and see if account name is in list
            foreach (Account account in _accounts)
            {
                if (account.Name == name)
                {
                    Console.WriteLine();
                    Console.WriteLine("Account Found.");
                    return account;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Account not Found.");
            return null;
        }



        public void ExecuteTransaction(Transaction transaction)
        {
            // add the attempt to the transactions list
            _transactions.Add(transaction);

            try
            {
                // attempt to execute transaction
                transaction.Execute();
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
            

        public void RollbackTransaction(Transaction transaction)
        {
            try
            {
                transaction.Rollback();
                Console.WriteLine();
                Console.WriteLine("Rollback Successful.");
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
        }

        public void PrintTransactionHistroy()
        {
            List<Transaction> transactions = TransactionList;

            if (transactions.Count != 0)
            {
                Console.WriteLine();
                Console.WriteLine("You Have Selected to Print the Transaction List");
                Console.WriteLine();

                Console.WriteLine("     |\t{0, -20}|\t{1,-25}|\t{2,-35}|\t{3,-25}|\t{4,-25}|\t{5,-25}|", "Transaction #", "Account Name", "Transaction Type", 
                    "Transaction Amount", "Time Attempted", "Success Status");
                Console.WriteLine(new string(' ', 5) + new string('-', 189));

                for (int i = 0; i < transactions.Count; i++)
                {

                    Console.Write("     |\t   {0,-17}|", i);

                    Console.Write("\t{0,-25}|", transactions[i].AccountName());

                    if (transactions[i] is DepositTransaction)
                    {
                        Console.Write("\t{0,-35}|", "Deposit");

                    }
                    else if (transactions[i] is WithdrawTransaction)
                    {
                        Console.Write("\t{0,-35}|", "Withdrawal");
                    }
                    else
                    {
                        Console.Write("\t{0,-35}|", $"Transfer to {transactions[i].DestinationAccount()}'s Account");
                    }

                    Console.Write("\t{0,-25}|", transactions[i].GetAmount());

                    Console.Write("\t{0,-25}|", transactions[i].DateStamp);


                    if (transactions[i].Reversed)
                    {
                        Console.WriteLine("\t{0,-25}|", "Reversed");
                    }
                    else if (transactions[i].Success)
                    {
                        Console.WriteLine("\t{0,-25}|", "Success");
                    }
                    else
                    {
                        Console.WriteLine("\t{0,-25}|", "Failed");
                    }
                }           
            }  
            else
            {
                throw new IndexOutOfRangeException("There are no Transactions to Print. Please Add a Transaction First.");
            }
        }
    }
}
