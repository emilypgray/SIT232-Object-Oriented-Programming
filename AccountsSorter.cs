using System;
using System.Collections.Generic;
using System.Linq;

namespace _3._4DT
{
    static public class AccountsSorter
    {
        static public void Sort(Account[] accounts, int b)
        {
            // Create new array of b empty buckets
            List<Account>[] buckets = new List<Account>[b];

            // Fill each bucket with an empty list
            for (int i = 0; i < b; i++)
            {
                buckets[i] = new List<Account>();
            }

            // Find the value of the maximum balance value in the input
            // accounts
            decimal maxKey = (from x in accounts select x.Balance).Max();

            // Apply the bucket sort algorithm. Calculate the appropriate bucket
            // to place each account of the input into and add to the corresponding
            // bucket list in the bucket array
            for (int i = 0; i < accounts.Length; i++)
            {
                int index = Convert.ToInt32(Math.Floor(b * accounts[i].Balance / maxKey));
                // If the value is the max value, place it into the last bucket
                if (index == b)
                    index = b - 1;
                buckets[index].Add(accounts[i]);
            }

            // Use the list sort function to sort the values in each bucket into the correct order
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i].Sort((x, y) => x.Balance.CompareTo(y.Balance));
            }

            Console.WriteLine();
            Console.WriteLine("The Accounts Sorted by Ascending Balance:");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("{0,-25}|\t{1}", "Name", "Balance");
            Console.WriteLine("-------------------------------------------");

            // Use for loop to iterate through each element in the bucket array and print out value 
            // to the console
            for (int i = 0; i < buckets.Length; i++)
            {
                for (int j = 0; j < buckets[i].Count; j++)
                    Console.WriteLine("{0,-25}|\t{1,5}", buckets[i][j].Name, buckets[i][j].Balance);                    
            }
   
        }




        // Same method - takes accounts as a list instead of in an array
        static public void Sort(List<Account> accounts, int b)
        {
            List<Account>[] buckets = new List<Account>[b];

            for (int i = 0; i < b; i++)
            {
                buckets[i] = new List<Account>();
            }

            decimal maxKey = (from x in accounts select x.Balance).Max();

            for (int i = 0; i < accounts.Count; i++)
            {
                int index = Convert.ToInt32(Math.Floor(b * accounts[i].Balance / maxKey));
                if (index == b)
                    index = b - 1;
                buckets[index].Add(accounts[i]);
            }

            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i].Sort((x, y) => x.Balance.CompareTo(y.Balance));
            }

            Console.WriteLine();
            Console.WriteLine("The Accounts Sorted by Ascending Balance:");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("{0,-25}|\t{1}", "Name", "Balance");
            Console.WriteLine("-------------------------------------------");

            for (int i = 0; i < buckets.Length; i++)
            {
                for (int j = 0; j < buckets[i].Count; j++)
                    Console.WriteLine("{0,-25}|\t{1,5}", buckets[i][j].Name, buckets[i][j].Balance);
            }
        }
    }
}
