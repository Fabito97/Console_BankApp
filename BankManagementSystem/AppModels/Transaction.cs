using BankManagementSystem.Enums;
using BankManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.AppModels
{
    public class Transaction
    {
        public Guid TransactionId { get; set; } = Guid.NewGuid();
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public decimal Balance { get; set; }
        public DateTime Date { get; set; }


        public Transaction(string accountNumber, DateTime date, decimal amount, TransactionType type, decimal lastBalance)
        {
            AccountNumber = accountNumber;
            Balance = lastBalance;
            Type = type;
            Amount = amount;
            Date = DateTime.Now;
        }
        
    }

}
