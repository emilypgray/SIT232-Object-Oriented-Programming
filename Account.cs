using System;
namespace _6._2___Multiple_Bank_Accounts
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
            set { _balance = value; }
            get { return _balance; }
        }

        public string Name
        {
            set { _name = value; }
            get { return _name; }

        }

        // Set up print method to print account information
        public void Print()
        {
            Console.WriteLine("\nAccount Name: {0}", _name);
            Console.WriteLine("Account Balance: {0}", _balance.ToString("C"));
        }
    }
}
