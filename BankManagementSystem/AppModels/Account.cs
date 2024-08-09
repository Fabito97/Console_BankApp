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
        Customer customer = new Customer();
        public string AccountHolder { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }

        public Account(string accountNumber, string accountName, AccountType accountType, decimal initialBalance) 
        {
            AccountHolder = accountName;
            AccountNumber = accountNumber;
            AccountType = accountType; 
            Balance = initialBalance;
        }       

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
       
               
    }
}
