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
                        HandleDeposit(saving, customerAccounts);
                        break;

                    case "4":

                        HandleWithdrawal(saving, current, customerAccounts);
                        break;

                    case "5":
                       /* HandleTransfer(saving, current, customerAccounts);*/
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

        private static void HandleDeposit(Account saving, List<Account> customerAccounts)
        {
            string accountNumber = GetSelectedAccountNumber(customerAccounts);

            var account = customerAccounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
            Console.WriteLine(account.AccountName);

            decimal amount = GetConvertedAmountInput();

            if (account == null)
                Utitily.PrintMessage("Account not found. Please check the account number and try again", false);

            else
            {
                saving.Deposit(account, amount);

                Utitily.PrintMessage($"\nN{amount} successfully credited to {account.AccountName}");

                var deposit = new AppModels.Transaction(account.AccountNumber, DateTime.Now, amount, TransactionType.Deposit, account.Balance);

                new TransactionService(FilePath.Transaction).AddTransaction(deposit);

            }
            Utitily.PressEnterToContinue();
            Console.Clear();

        }

        private static decimal GetConvertedAmountInput()
        {
            string amount;

            while (true)
            {
                amount = Utitily.GetUserInput("the amount you wish to deposit");
                Console.WriteLine();

                if (int.TryParse(amount, out int ValidAmount))
                {
                    break;
                }
                else
                {
                    Utitily.PrintMessage("Invalld input, please enter a valid amount\n", false);
                }
            }

            return Convert.ToDecimal(amount);
        }

        private static void HandleWithdrawal(Account saving, Account current, List<Account> customerAccounts)
        {
            
            string accountNumber = GetSelectedAccountNumber(customerAccounts);

            var account = customerAccounts.FirstOrDefault(x => x.AccountNumber == accountNumber);

            decimal withdrawalAmount = GetConvertedAmountInput();



            bool isSuceess = saving.Withdraw(account, withdrawalAmount);

            if (isSuceess)
            {

                Utitily.PrintMessage($"\nN{withdrawalAmount} successfully debited from your {account.AccountType}. " +
                          $"\nYour new balance is N{account.GetBalance()}", true);

                var withdraw = new AppModels.Transaction(account.AccountNumber, DateTime.Now, withdrawalAmount, TransactionType.Withdrawal, account.Balance);

                var transactionService = new TransactionService(FilePath.Transaction);

                transactionService.AddTransaction(withdraw);
            }

            Utitily.PressEnterToContinue();
            Console.Clear();

        }

        private static void HandleTransfer(Account saving, Account current, List<Account> customerAccounts)
        {
            // Get the desired transaction account
            string sourceAccountNo = GetSelectedAccountNumber(customerAccounts);
            var sourceAccount = customerAccounts.FirstOrDefault(acc => acc.AccountNumber == sourceAccountNo);

            string targetAccountNo = Utitily.GetUserInput("the destination account number ");

            var targetAccount = AccountService.GetAccounts().FirstOrDefault(acc => acc.AccountNumber == targetAccountNo);

            if (targetAccount == null)
            {
                Utitily.PrintMessage("Account not found. Please check the account number and try again", false);
            }
            else
            {
                Console.WriteLine($"Account Name: {targetAccount.AccountName}");

                decimal amount = GetConvertedAmountInput();

                saving.Transfer(sourceAccount, targetAccount, amount);

            }
            Utitily.PressEnterToContinue();
            Console.Clear(); 

        }

        public static string GetSelectedAccountNumber(List<Account> customerAccounts)
        {            

            if (customerAccounts.Count == 1)
            {
                return customerAccounts[0].AccountNumber;
            } 
           
            int selectedOption;

            do
            {
                Console.WriteLine("---------SELECT AN ACCOUNT---------\n");

                int n = customerAccounts.Count;

                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine($"{i + 1}. {customerAccounts[i].AccountType} - {customerAccounts[i].AccountNumber}");
                }
                Console.WriteLine();

                var input = Utitily.GetUserInput("an option to proceed");

                if (int.TryParse(input, out selectedOption) && selectedOption >= 1 && selectedOption <= n)
                {
                    break;
                }

                Utitily.PrintMessage("\nInvalid option, please try again", false);

                Utitily.PressEnterToContinue();
                Console.Clear();

            } while (true);

            return  customerAccounts[selectedOption - 1].AccountNumber;
                
        }
    }
}
