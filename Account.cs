using System;
using System.Collections.Generic;

namespace _3._4DT
{
    public class Account
    {
        // Declare private instance variables
        private decimal _balance;
        private string _name;

        // Set up the constructor to initialise instance variables
        public Account(string name, decimal balance)
        {
            _balance = balance;
            _name = name;
        }

        // Set up accessor methods for Balance and Name
        // Set set accessor to private so they cannot be changed
        public decimal Balance
        {
            private set { _balance = value; }
            get { return _balance; }
        }

        public string Name
        {
            private set { _name = value; }
            get { return _name; }

        }

        // METHODS
        public void Deposit()
        {
            bool depositValid = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Please enter amount to deposit: ");

                // Check that input is valid. If it is, make the deposit, otherwise
                // print an error message and prompt user to input again
                try
                {
                    decimal deposit = Convert.ToDecimal(Console.ReadLine());
                    // Subtract withdrawal from balance if withdraw amount <= balance
                    if (deposit > 0)
                    {
                        depositValid = true;
                        _balance += deposit;
                        Console.WriteLine();
                        Console.WriteLine("Deposit Successful.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input. Please enter a positive number.");
                    }                        
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            } while (!depositValid);
        }


        public void Withdraw()
        {
            bool withdrawValid = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Please enter amount to withdraw: ");
                try
                {
                    // Check that input is valid. If it is, make the withdrawal, otherwise
                    // print an error message and prompt user to input again
                    decimal withdrawal = Convert.ToDecimal(Console.ReadLine());
                    if (withdrawal <= _balance && withdrawal > 0)
                    {
                        withdrawValid = true;
                        _balance -= withdrawal;
                        Console.WriteLine();
                        Console.WriteLine("Withdrawal Successful.");
                    }  
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Cannot withdraw amount larger than balance. Please try again");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            } while (!withdrawValid);                        
        }


        // Set up print method to output the name and balance to terminal
        public void Print()
        {
            Console.WriteLine("\nAccount Name: {0}", _name);
            Console.WriteLine("Account Balance: {0}", _balance.ToString("C"));
        }

            
    }
}
