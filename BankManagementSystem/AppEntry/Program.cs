using BankManagementSystem.App;
using BankManagementSystem.Enums;
using BankManagementSystem.Services;
using System.Transactions;

namespace BankManagementSystem.AppEntry
{
    public class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();

            List<Customer> customers = new List<Customer>();
            List<Account> accounts = new List<Account>();

            List<Transactions> transactionsList = new List<Transactions>();

            Account current = ScreenDisplay.PrintAccountDetails(customers, accounts);

            ScreenDisplay.PrintAccountStatement(current, transactionsList);

        }

      
    }
}
