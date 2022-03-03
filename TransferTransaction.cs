using System;
using System.Collections.Generic;
using System.Text;

namespace _6._2___Multiple_Bank_Accounts
{
    class TransferTransaction : Transaction
    {

        private Account _accountFrom;
        private Account _accountTo;
        private WithdrawTransaction _withdraw;
        private DepositTransaction _deposit;


        // set up constructor to create a new object each of the WithdrawTransaction and DepositTransaction classes
        public TransferTransaction(Account accountFrom, Account accountTo, decimal amount) : base(amount)
        {
            _accountFrom = accountFrom;
            _accountTo = accountTo;
            _amount = amount;
            _withdraw = new WithdrawTransaction(_accountFrom, amount);
            _deposit = new DepositTransaction(_accountTo, amount);
        }

        public Account AccountFrom { get { return _accountFrom; } }
        public Account AccountTo { get { return _accountTo; } }

        public override string AccountName()
        {
            return _accountFrom.Name;
        }

        public override string DestinationAccount()
        {
            return _accountTo.Name;
        }

        public override void Execute()
        {
            try
            {
                _withdraw.Execute();
                try
                {
                    _deposit.Execute();
                    base._success = true;
                    Print();
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }


        public override void Rollback()

        {
            // execute base method and then each individual transaction separately
            base.Rollback();

            _withdraw.Rollback();

            _deposit.Rollback();
        }


        public override void Print()
        {
            // print message if transfer was successful
            if (_withdraw.Success && _deposit.Success)
            {
                Console.WriteLine();
                Console.WriteLine("Successfully transferred {0:c} from {1}'s account to {2}'s account", _amount, _accountFrom.Name, _accountTo.Name);
            }
            else
            {
                Console.WriteLine("Transfer failed.");
            }
        }

    }
}
