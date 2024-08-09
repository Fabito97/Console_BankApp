using BankManagementSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.App
{
    public class Transactions
    {
        public int TransactionId { get; set; }
        public Account Account { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public decimal Balance { get; set; }
        public DateTime Date { get; set; }

       
        public Transactions(DateTime date, decimal amount, TransactionType type, Decimal lastBalance)
        {
            Balance = lastBalance;
            Type = type;
            Amount = amount;
            Date = DateTime.Now;
        }
    }


}
