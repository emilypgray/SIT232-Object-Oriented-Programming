using System;
using System.Collections.Generic;
using System.Text;

namespace _6._2___Multiple_Bank_Accounts
{
    abstract class Transaction
    {
        protected Decimal _amount; 
        protected Boolean _success = false;
        private Boolean _executed = false;
        private Boolean _reversed = false;
        private DateTime _dateStamp = DateTime.Now;

        public Boolean Success { get { return _success; } }
        public Boolean Executed { get { return _executed; } }
        public Boolean Reversed { get { return _reversed; } }
        public DateTime DateStamp {  get { return _dateStamp; } }

        public Transaction(Decimal amount) 
        {
            _amount = amount;
        }

        public string GetAmount() 
        { 
            return string.Format("{0:C}", _amount);
        } 


        // create an abstract method to return account name
        abstract public string AccountName();

        abstract public string DestinationAccount();

        abstract public void Print();

        virtual public void Execute() 
        {
            // calculate time elapsed between instantiation and now so that the difference can be added to the _dateStamp variable
            TimeSpan elapsedTime = DateTime.Now - _dateStamp;
            _dateStamp = _dateStamp.Add(elapsedTime);

            if (_executed)
            {
                // if transaction has already been executed, throw exception
                Console.WriteLine();
                throw new InvalidOperationException("Error: Unable to perform transaction. Transaction has already been attempted.");
            }
            else if (_amount < 1)
            {
                Console.WriteLine();
                throw new InvalidOperationException("Error: Cannot transact on amount less than $1.00.");
            }
            else
            {
                //set executed to true if no exception is thrown
                _executed = true;
                
            }
        }


        virtual public void Rollback()
        {
            TimeSpan elapsedTime = DateTime.Now - _dateStamp;
            _dateStamp = _dateStamp.Add(elapsedTime);

            // if transaction was successful
            if (!_success)
            {
                throw new InvalidOperationException("Error: Rollback failed. Previous transaction was unsuccesful and cannot be reversed.");
            }

            if (_reversed)
            {
                throw new InvalidOperationException("Error: Rollback failed. Transaction has already been reversed.");
            }

            _reversed = true;
      
        }  
    }
}
