using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BankManagementSystem.App;
using BankManagementSystem.Enums;

namespace BankManagementSystem
{
    public class Saving : Account
    {
        List<Transactions> TransactionList;

        public decimal MinBalance { get; }

        public Saving(string accountNumber, string accountName, AccountType accountType, decimal minbalance = 1000m) : 
            base(accountNumber, accountName, AccountType.Saving, minbalance) 
        {
            MinBalance = minbalance;
            TransactionList = new List<Transactions>();
        }              

        public override void Deposit(Decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("\nInvalid Transaction.");
            }
            else if (amount < 10)
            {
                Console.WriteLine("\nAmount to be deposited must be up to 10 naira");
            }
            else 
            {
                Balance += amount;
                Console.WriteLine($"\nDeposit of N{amount} into Saving Account is successful. Your new balance is N{Balance:c}");


                var deposit = new Transactions(DateTime.Now, amount, TransactionType.Deposit, GetBalance());
                TransactionList.Add(deposit);
            }
        } 
        public override void Withdraw(Decimal amount)
        {
            if (amount < 10)
            {
                Console.WriteLine("\nInvalid Transaction. Amount must be up to N10");
            }
            else if (amount > Balance - MinBalance) 
            {
                Console.WriteLine($"\ninsufficient Balance. Cannot withdraw N{amount} because minimum Balance must be maintained");
            }            
            else if (amount <= Balance - MinBalance)
            {
                Balance -= amount;
              
                Console.WriteLine($"\nWithdrawal of N{amount} out of your Saving Account is successful. Your new balance is N{Balance}");
               

                var withdraw = new Transactions(DateTime.Now, amount, TransactionType.Withdrawal, GetBalance());
                TransactionList.Add(withdraw);
            }
            
        }

        public override void Transfer(Account targetAccount, decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("\nInvalid Transaction. Amount must be a positive number");
            }
            else if (amount > Balance - MinBalance)
            {
                Console.WriteLine($"\ninsufficient Balance. Cannot withdraw N{amount}");
            }
            else 
            {
                targetAccount.Balance += amount;

                Console.WriteLine($"\nTransfer of N{amount} from your Saving Account is successful. Your new balance is N{Balance}");

                var withdraw = new Transactions(DateTime.Now, amount, TransactionType.Withdrawal, GetBalance());
                TransactionList.Add(withdraw);
            }
        }

        public void GetAccountStatement(string accountNumber)
        {
            Console.WriteLine("\nACCOUNT STATEMENT ON ACCOUNT NO 0987654321");
            Console.WriteLine("+-------------------------------------------------------------------+");
            Console.WriteLine("|   DATE\t  | DESCRIPTION | AMOUNT |  BALANCE |");
            Console.WriteLine("+-------------------------------------------------------------------+");
           
            foreach (var item in TransactionList)
            {
                if (item.Account.AccountNumber == accountNumber)
                {
                    Console.WriteLine($"| {item.Date} | {item.Type} | {item.Amount} | {item.Balance}");
                    Console.WriteLine("+-------------------------------------------------------------------+");

                }
            }
            //var transaction = TransactionList.Where(x => x.Equals(accounNumber)).FirstOrDefault();
            //Console.WriteLine($"| {transaction.Date} | {transaction.Type} | {transaction.Amount} | {transaction.Balance}");
        }


        





    }
}
