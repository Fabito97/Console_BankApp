using BankManagementSystem.Data;
using BankManagementSystem.Enums;
using BankManagementSystem.UI;
using Newtonsoft.Json;

namespace BankManagementSystem.Services
{
    public class AccountService
    {
        private readonly string filePath;

        public AccountService() { }

        public AccountService(string path) 
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            filePath = path;

            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.WriteAllText(filePath, "[]");
            }
        }



		public static string GenerateAccountNumber()
		{
			Random accountNumber = new Random();
			return accountNumber.NextInt64(1000000000, 9999999999).ToString();
		}


		public static void CreateAccount(List<Account> customerAccounts, Customer loggedInCustomer)
        {

            if (loggedInCustomer == null)
            {
                Console.WriteLine("You need to be a registered customer to open an account");
                return;
            }

            Console.WriteLine("-------------------CREATE A NEW ACCOUNT-------------------\n");

            Account newAccount = GetAccountSelectionType(loggedInCustomer.FullName, loggedInCustomer.Id);

            new AccountService(FilePath.Account).AddAccountToFile(newAccount);

            customerAccounts.Add(newAccount);

            Utitily.PrintMessage($"Account created successfully!");

            Console.WriteLine($"\nAccount Name: {newAccount.AccountName}" +
            $"\nAccount Number: {newAccount.AccountNumber}, " +
            $"\nAccount Type: {newAccount.AccountType}, " +
            $"\nInitial Balance: {newAccount.Balance}");


            Utitily.PressEnterToContinue();
            Console.Clear();
        }

        private static Account GetAccountSelectionType(string customerName, Guid customerId)
        {
            while (true)
            {

                Console.WriteLine("1. Current Account");
                Console.WriteLine("2. Saving Account");
                Console.WriteLine();

                string input = Utitily.GetUserInput("the type account you wish to open");

                if (input == "1")
                {
                    return new Current(customerId, customerName, GenerateAccountNumber(), AccountType.Current, 0);                     

                }
                else if (input == "2")
                {
                    return new Saving(customerId, customerName, GenerateAccountNumber(), AccountType.Current, 0);
                }
               
                Utitily.PrintMessage("Invalid input, please try again", false);

                Utitily.PressEnterToContinue();
               
            }

        }

        public void AddAccountToFile(Account account)
        {
            try
            {
                var accounts = new List<Account>();

                var jsonData = File.ReadAllText(filePath);

                accounts = JsonConvert.DeserializeObject<List<Account>>(jsonData) ?? new List<Account>();

                accounts.Add(account);

                string updatedFile = JsonConvert.SerializeObject(accounts, Formatting.Indented);

                File.WriteAllText(filePath, updatedFile);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateAccounts(Account updatedAccount)
        {
            try
            {                
                var jsonData = File.ReadAllText(FilePath.Account);

                var accounts = JsonConvert.DeserializeObject<List<Account>>(jsonData) ?? new List<Account>();

                var account = accounts.FirstOrDefault(x => x.Id == updatedAccount.Id);

                account.Balance = updatedAccount.Balance;

                string updatedFile = JsonConvert.SerializeObject(accounts, Formatting.Indented);

                File.WriteAllText(FilePath.Account, updatedFile);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Account> GetAccounts()
        {  
            var accounts = new List<Account>();

            if (File.Exists(FilePath.Account))
            {
                var jsonData = File.ReadAllText(FilePath.Account);
                accounts = JsonConvert.DeserializeObject<List<Account>>(jsonData) ?? new List<Account>();
            }
            return accounts;
        }

        public static void GetAccountDetails(List<Account> customerAccounts)
        {

            if (customerAccounts.Count == 0)
            { 
                Utitily.PrintMessage("You don't have any account yet. Please proceed to open an account", false);
            }
            else
            {
                Console.WriteLine("\nACCOUNT DETAILS");
                Console.WriteLine("+-------------------------------------------------------------------+");
                Console.WriteLine("|   FULL NAME\t  | ACCOUNT NUMBER | ACCOUNT TYPE | ACCOUNT BALANCE |");
                Console.WriteLine("+-------------------------------------------------------------------+");
                foreach (var account in customerAccounts)
                {
                    Console.WriteLine($"| {account.AccountName} | {account.AccountNumber}     | {account.AccountType}\t  | N{account.Balance}\t    |");
                    Console.WriteLine("+-------------------------------------------------------------------+");

                }
            }

            Utitily.PressEnterToContinue();
            Console.Clear();

        }

        public string PrintAllAccounts()
        {
            var accounts = GetAccounts();

            if (accounts != null)
            {
                Console.WriteLine("Account Name \t | Account Number \t | Account Type \t | Account Balance");
                foreach (Account account in accounts)
                {
                   return $"{account.AccountName} | {account.AccountNumber} | {account.AccountType} | N{account.Balance} |";
                }
            }
            return "Account List is empty";
        }

    }
}
