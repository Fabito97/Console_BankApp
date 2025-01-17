using BankManagementSystem.AppModels;
using BankManagementSystem.Data;
using BankManagementSystem.Enums;
using BankManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankManagementSystem.UI
{
    public class AppScreen
    {
        public static void CustomerDashboard(List<Account> customerAccounts, Customer loggedinCustomer)
        {
            Account saving = new Saving();
            Account current = new Current();
            
            do
            {
                Console.WriteLine($"Hi {loggedinCustomer.FullName}, what would you like to do today? \t {DateTime.Now.ToString("d")}\n");

                DisplayAccounts(customerAccounts);

            } while (AccountOperations(saving, current, customerAccounts, loggedinCustomer));

        }

        private static void DisplayAccounts(IEnumerable<Account> customerAccounts)
        {

            Console.WriteLine("--------------------------------------------");
            foreach (var account in customerAccounts)
            {
                Console.WriteLine($"{account.AccountType} - {account.AccountNumber} \t\t- {account.GetBalance()}");
            }
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();
        }

        public static bool AccountOperations(Account saving, Account current, List<Account> customerAccounts, Customer loggedinCustomer)
        {              
                bool isRunning = true;

                Console.WriteLine("1. Create a new account");
                Console.WriteLine("2. Get Account Details");
                Console.WriteLine("3. Make Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. Get Account Statement");
                Console.WriteLine("7. LogOut\n");

                string input = Utitily.GetUserInput("an option to proceed");
                Console.WriteLine();
                Console.Clear();              

                switch (input)
                {
                    case "1":
                        AccountService.CreateAccount(customerAccounts, loggedinCustomer);    
                        break;

                    case "2":
                        AccountService.GetAccountDetails(customerAccounts);                    
                        break;

                    case "3":
					AccountOperationManager.HandleDeposit(saving, customerAccounts);
                        break;

                    case "4":

                        AccountOperationManager.HandleWithdrawal(saving, current, customerAccounts);
                        break;

                    case "5":
                        AccountOperationManager.HandleTransfer(saving, current, customerAccounts);
                        break;

                    case "6":
                        
                        TransactionService.GetAccountStatement(customerAccounts);                   
                        break;
                   
                    case "7":
                        isRunning = false;
                        CustomerAuthService.Logout();
                        break;
                   
                    default:
                        Utitily.PrintMessage("Invalid option.", false);
                   
                        Utitily.PressEnterToContinue();

                        break;
                }

                return isRunning;
        }

      
    }
}
