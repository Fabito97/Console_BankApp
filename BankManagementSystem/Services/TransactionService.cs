using BankManagementSystem.AppModels;
using BankManagementSystem.Data;
using BankManagementSystem.Enums;
using BankManagementSystem.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Services
{
    public class TransactionService
    {

        private readonly string filePath;

        public TransactionService(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path)); 

            filePath = path;

            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.WriteAllText(filePath, "[]");
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            try
            {
                var transactions = new List<Transaction>();

                string jsonData = File.ReadAllText(filePath);
                transactions = JsonConvert.DeserializeObject<List<Transaction>>(jsonData) ?? new List<Transaction>();          

                transactions.Add(transaction);

                var updatedFile = JsonConvert.SerializeObject(transactions, Formatting.Indented);

                File.WriteAllText(filePath, updatedFile);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static List<Transaction> GetTransactions()
        {
            try
            {
                var transactions = new List<Transaction>();

                if (File.Exists(FilePath.Transaction))
                {
                    string jsonData = File.ReadAllText(FilePath.Transaction);

                    transactions = JsonConvert.DeserializeObject<List<Transaction>>(jsonData) ?? new List<Transaction>();                    
                }
                return transactions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void GetAccountStatement(List<Account> customerAccounts)
        {
            string accountNumber = null;

            if (customerAccounts.Count == 1)
            {
                accountNumber = customerAccounts[0].AccountNumber;
            }
            else
            {
                 accountNumber = Utitily.GetSelectedAccountNumber(customerAccounts);
            }

            var transactionAccount = customerAccounts.FirstOrDefault(x => x.AccountNumber == accountNumber);

            if (transactionAccount != null)
            {
                var transactions = GetTransactions().Where(x => x.AccountNumber == transactionAccount.AccountNumber);

                if (transactions.Any())
                {
                    Console.WriteLine($"\nACCOUNT STATEMENT ON ACCOUNT NO {accountNumber}");
                    Console.WriteLine("+-------------------------------------------------------------------+");
                    Console.WriteLine("|        DATE         |   DESCRIPTION    |   AMOUNT    |  BALANCE   |");
                    Console.WriteLine("+-------------------------------------------------------------------+");
                    foreach (var transaction in transactions)
                    {
                        Console.WriteLine($"| {transaction.Date} | {transaction.Type}\t \t| {transaction.Amount} \t| N{transaction.Balance}");
                        Console.WriteLine("+-------------------------------------------------------------------+");
                    }
                }
                else Utitily.PrintMessage("\nYou have not made any transaction with this account yet", false);
                
            }
            else Console.WriteLine("No matching account found");

            Utitily.PressEnterToContinue();
            Console.Clear();

   
        }

       
    }
}
