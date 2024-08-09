using BankManagementSystem.App;
using BankManagementSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace BankManagementSystem.Services
{
    public class CustomerService
    {
        Customer customer = new Customer();

        public CustomerService()
        {
            
        }

        private bool IsValidEmail(string email)
        {
            string validPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, validPattern);
        }

        public string SetEmail(string email)
        {
            while (!IsValidEmail(email))
            {
                Console.WriteLine("Invalid email format.");
                Console.WriteLine("\nEnter your first Name");
                email = Console.ReadLine();
            }
            return customer.Email = email;
        }

        private bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*[@#$%^&!]).{6,}$";
            return Regex.IsMatch(password, pattern);
        }

        public string SetPassword(string password)
        {
            while (!IsValidPassword(password))
            {
                Console.WriteLine("Password must be at least 6 characters long, contain an uppercase letter, and include a special character.");
                Console.WriteLine("\nEnter your first Name");
                password = Console.ReadLine();
            }
            return customer.Password = password;
        }

        public bool Authenticate(string email, string password)
        {
            return customer.Email.ToLower() == email.ToLower() && customer.Password == password;
        }

        public void Registercustomer(string firstName, string lastName, string email, string password)
        {
            Customer newCustomer = new Customer(firstName, lastName, email, password);
            customer.AddCustomer(newCustomer);

            Console.WriteLine("Customer registered successfully");
            Console.WriteLine($"Full Names: {newCustomer.FirstName} {newCustomer.LastName}");
            Console.WriteLine($"Email: {newCustomer.Email}");
        }

        public void CreateAccount(string accountNumber, string accountName, AccountType accountType) 
        {
            Account newAccount = null;
            accountName = customer.Fullname;
            string acctno = Account.GenerateAccountNumber();

            if (accountType == AccountType.Current) 
            {
                 newAccount = new Current(acctno, accountName,AccountType.Current, 0);       
                               
            }
            else
            {
                 newAccount = new Saving(acctno, accountName,AccountType.Saving, 1000);
            }

            customer.CreateAccount(newAccount);
            Console.WriteLine($"Account created successfully! Account Number: {newAccount.AccountNumber}, Account Type: {newAccount.AccountType}, Initial Balance: {newAccount.Balance}");                              
        }

        public Customer Login(string email, string password) 
        {
            foreach (var item in customer.GetCustomers())
            {
                if (Authenticate(email, password)) 
                {
                    Console.WriteLine("Login Successful");
                        return customer;
                }
            }
            Console.WriteLine("Invalid email or password.");
            return null;
        }

        public void GetAccountDetails(string customerName)
        {
            Console.WriteLine("\nACCOUNT DETAILS");
            Console.WriteLine("+-------------------------------------------------------------------+");
            Console.WriteLine("|   FULL NAME\t  | ACCOUNT NUMBER | ACCOUNT TYPE | ACCOUNT BALANCE |");
            Console.WriteLine("+-------------------------------------------------------------------+");

            if (customer.Accounts.Count < 1)
            {
                foreach (var account in customer.Accounts)
                {
                    if (account.AccountHolder == customer.Fullname)
                    {
                        Console.WriteLine($"| {account.AccountHolder} | {account.AccountNumber} {account.AccountType}t  | {account.Balance} |");
                        Console.WriteLine("+-------------------------------------------------------------------+");
                    }
                    Console.WriteLine("No account found");

                }
            }
            Console.WriteLine("Account List is empty");

        }
    }
}
