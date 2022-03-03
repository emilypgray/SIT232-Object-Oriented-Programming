using System;
using System.Collections.Generic;
using System.Text;

namespace _6._2___Multiple_Bank_Accounts
{
    class DepositTransaction : Transaction
    {
        //declare private instance variables
        private Account _account;

        // set up constructor to create new object
        public DepositTransaction(Account account, decimal amount) : base(amount)
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
            
            try
            {
                base.Execute();
                _account.Balance += _amount;
                base._success = true;
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
            }                    
        }

        public override void Print()
        {
            // if transaction was successful, print message
            if (base._success)
            {
                Console.WriteLine();
                Console.WriteLine("Deposit was Successful. {0:c} deposited into {1}'s account.", _amount, _account.Name);
            }

        }

        public override void Rollback()
        {
            // if transaction was successful

            base.Rollback();
            _account.Balance -= _amount;
        }

        +
        public override string DestinationAccount()
        {
            return null;
        }

    }
}
