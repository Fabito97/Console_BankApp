using BankManagementSystem.AppModels;
using BankManagementSystem.Enums;
using BankManagementSystem.Services;
using BankManagementSystem.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using BankManagementSystem.Data;
using BankManagementSystem.UI;

namespace BankManagementSystem
{
    public class Account
    {

        private readonly decimal MinBalance = 1000m;

        // Customer customer = new Customer();
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; private set; }
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }

        public Account() : base() 
        { 

        }

        public Account(Guid customerId, string accountName, string accountNumber, AccountType accountType, decimal initialBalance) 
        {
            CustomerId = customerId;
            AccountName = accountName;
            AccountType = accountType; 
            Balance = initialBalance; 
            AccountNumber = accountNumber;
        }

        public virtual bool Deposit(Account account, decimal amount)
        {
            if (account == null)
            {
                Utitily.PrintMessage("Account not found. Please check the account number and try again", false);
                return false;
            }

            if (amount < 100)
            {
                Utitily.PrintMessage("\nInvalid Transaction. Amount must be up to N100", false);
                return false;
            }
            else
            {
                account.Balance += amount;

                AccountService.UpdateAccounts(account);

                return true;
            }
             
        }


        public virtual bool Withdraw(Account account, decimal amount)
        {

            if (account == null)
            {
                Utitily.PrintMessage("Account not found. Please check the account number and try again", false);
                return false;
            }

            if (amount < 100)
            {
                Utitily.PrintMessage("\nInvalid Transaction. Amount must be up to N100", false);
                return false;
            }

            bool isValidAmount = amount < account.Balance;

            if (account.AccountType == AccountType.Saving)
            {
                isValidAmount = amount < account.Balance - MinBalance;
            }

            if (!isValidAmount)
            {
                Utitily.PrintMessage($"\nMinimum Balance of N1000 must be maintained", false);
                return false;
            }

            account.Balance -= amount;

            AccountService.UpdateAccounts(account);

            return true;
        }


        public virtual void Transfer(Account sourceAccount, Account targetAccount, decimal amount)
        {

            if (sourceAccount == null || targetAccount == null)
            {
                Utitily.PrintMessage("Account not found. Please check the account number and try again", false);
                return;
            }

            if (sourceAccount.Withdraw(sourceAccount, amount))
            {
                if (targetAccount.Deposit(targetAccount, amount))
                {
                    Utitily.PrintMessage($"\nN{amount} successfully transferred to {targetAccount.AccountName}. " +
                        $"\nYour new balance is N{sourceAccount.GetBalance()}", true);

                    // Create transaction log for both account
                    var transfer = new AppModels.Transaction(sourceAccount.AccountNumber, DateTime.Now, amount, TransactionType.Transfer, sourceAccount.Balance);
                    var credit = new AppModels.Transaction(targetAccount.AccountNumber, DateTime.Now, amount, TransactionType.Transfer, targetAccount.Balance);

                    // log the trans
                    var transactionService = new TransactionService(FilePath.Transaction);
                    transactionService.AddTransaction(transfer);
                    transactionService.AddTransaction(credit);

                }
                else
                {
                    // Reverse withdrawal
                    sourceAccount.Deposit(sourceAccount, amount);

                    // Handle withdrawal failure
                    Utitily.PrintMessage("Transfer failed. Unable to creadit the target account.", false);
                }

            }
            else
            {
                // Handle withdrawal failure
                Utitily.PrintMessage("Transfer failed. Please check your balance and try again.", false);
            }


        }

        public  string GetBalance()
        {
            return Balance.ToString("C", CultureInfo.CreateSpecificCulture("en-NG"));
        }

         
    }
}
