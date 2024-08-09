using BankManagementSystem.App;
using BankManagementSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem
{
    public abstract class Account
    {
       // private static int _AccountNumber = 1023847860;
       
        public Customer AccountHolder { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }

        public Account(string accountNumber, AccountType accountType) 
        {
            AccountNumber = accountNumber;
            AccountType = accountType; 
            Balance = 0;
        }

        //public abstract void GetAccountType();

        public abstract void Deposit(decimal amount);
        public abstract void Withdraw(decimal amount);
        public abstract void Transfer(Account account, decimal amount);

        public  decimal GetBalance()
        {
            return Balance;
        }

        public static string GenerateAccountNumber()
        {
            Random rnd = new Random();
            var accountNumber = rnd.NextInt64(0000000000, 9999999999);
            return accountNumber.ToString();
        }
        public void GetAccountDetails()
        {
            Console.WriteLine("\nACCOUNT DETAILS");
            Console.WriteLine("+-------------------------------------------------------------------+");
            Console.WriteLine("|   FULL NAME\t  | ACCOUNT NUMBER | ACCOUNT TYPE | ACCOUNT BALANCE |");
            Console.WriteLine("+-------------------------------------------------------------------+");
            foreach (var account in AccountHolder.Accounts)           
            {
                Console.WriteLine($"| {AccountHolder.Fullname} | {account.AccountNumber} {account.AccountType}t  | {account.Balance} |");
                Console.WriteLine("+-------------------------------------------------------------------+");
            }
           
        }      
               
    }
}
