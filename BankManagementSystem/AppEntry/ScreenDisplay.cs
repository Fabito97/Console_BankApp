using BankManagementSystem.App;
using BankManagementSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.AppEntry
{
    public class ScreenDisplay
    {
        public static Account PrintAccountDetails(List<Customer> customers, List<Account> accounts)
        {
            Account current = new Saving("1290284651", "Fabian Muoghalu", 23100);
            Account Saving = new Saving("1290284652", "Fabian Muoghalu", 10900);

            accounts.Add(current);
            accounts.Add(Saving);

            Console.WriteLine("\nACCOUNT DETAILS");
            Console.WriteLine("+-------------------------------------------------------------------+");
            Console.WriteLine("|   FULL NAME\t  | ACCOUNT NUMBER | ACCOUNT TYPE | ACCOUNT BALANCE |");
            Console.WriteLine("+-------------------------------------------------------------------+");

            foreach (var account in accounts)
            {
                Console.WriteLine($"| {account.AccountName} | {account.AccountNumber}\t  | {account.AccountType}\t  |   {account.Balance}\t    | ");
                Console.WriteLine("+-------------------------------------------------------------------+");
            }

            return current;
        }

        public static void PrintAccountStatement(Account current, List<Transactions> transactionsList)
        {
            Transactions withdrawal = new Transactions(DateTime.Now, 2200m, TransactionType.Withdrawal, current.Balance - 2200);
            Transactions Deposit = new Transactions(DateTime.Now, 4500m, TransactionType.Deposit, current.Balance + 4500);

            transactionsList.Add(withdrawal);
            transactionsList.Add(Deposit);

            Console.WriteLine("\nACCOUNT STATEMENT ON ACCOUNTNO: 1290284651");
            Console.WriteLine("+-------------------------------------------------------------------+");
            Console.WriteLine("|   DATE\t    | DESCRIPTION         | AMOUNT       |  BALANCE       |");
            Console.WriteLine("+-------------------------------------------------------------------+");
            foreach (var transaction in transactionsList)
            {
                Console.WriteLine($"{transaction.Date} | \t{transaction.Amount} \t\t | {transaction.Type} \t | {transaction.Balance}    |");
                Console.WriteLine("+-------------------------------------------------------------------+");

            }
        }
    }
}
