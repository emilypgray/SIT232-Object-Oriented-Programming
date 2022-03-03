using System;
using System.Collections.Generic;
using System.Text;

namespace _6._2___Multiple_Bank_Accounts
{
    class WithdrawTransaction : Transaction
    {
        // declare class private instance variables
        private Account _account;

        // set up constructor
        public WithdrawTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
            _amount = amount;
        }

        public override string AccountName()
        {
            return _account.Name;
        }


        public override void Execute()
        {
            if (_amount <= _account.Balance)
            {
                try
                {
                    // execute the base method and subtract the amount from the current method
                    base.Execute();
                    _account.Balance -= _amount;
                    base._success = true;
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            else
            {
                Console.WriteLine("Error: Insufficient funds to make withdrawal.");
            }
            
        }

        public override void Print()
        {
            // if transaction was successful, print message
            if (_success)
            {
                Console.WriteLine();
                Console.WriteLine("Withdrawal was Successful. {0:c} withdrawn from {1}'s account.", _amount, _account.Name);
            }
        }


        public override void Rollback()
        {
            // if transaction was successful

            base.Rollback();
            _account.Balance += _amount;
      
        }

        public override string DestinationAccount()
        {
            return null;
        }

    }
}
