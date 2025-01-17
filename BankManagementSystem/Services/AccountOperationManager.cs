using BankManagementSystem.Data;
using BankManagementSystem.Enums;
using BankManagementSystem.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Services
{
	public class AccountOperationManager
	{
		public static void HandleDeposit(Account saving, List<Account> customerAccounts)
		{
			string accountNumber = Utitily.GetSelectedAccountNumber(customerAccounts);

			var account = customerAccounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
			Console.WriteLine(account.AccountName);

			decimal amount = Utitily.ConvertAmountInput();

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

		public static void HandleWithdrawal(Account saving, Account current, List<Account> customerAccounts)
		{

			string accountNumber = Utitily.GetSelectedAccountNumber(customerAccounts);

			var account = customerAccounts.FirstOrDefault(x => x.AccountNumber == accountNumber);

			decimal withdrawalAmount = Utitily.ConvertAmountInput();



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

		public static void HandleTransfer(Account saving, Account current, List<Account> customerAccounts)
		{
			// Get the desired transaction account
			string sourceAccountNo = Utitily.GetSelectedAccountNumber(customerAccounts);
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

				decimal amount = Utitily.ConvertAmountInput();

				saving.Transfer(sourceAccount, targetAccount, amount);

			}
			Utitily.PressEnterToContinue();
			Console.Clear();

		}

	}
}
