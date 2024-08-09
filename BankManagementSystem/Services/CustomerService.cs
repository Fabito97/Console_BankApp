using BankManagementSystem.App;
using BankManagementSystem.Enums;
using BankManagementSystem.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankManagementSystem.Services
{
    public class CustomerService
    {
        
        private List<Customer> CustomerList;
        private List<Account> Accounts;

        public CustomerService()
        {
            
        }
               

        public void Registercustomer(string firstName, string lastName, string email, string password)
        {
            var newCustomer = new Customer(firstName, lastName, email, password);
            CustomerList.Add(newCustomer);

            Console.WriteLine("Customer registered successfully");
            Console.WriteLine($"Full Names: {newCustomer.FirstName} {newCustomer.LastName}");
            Console.WriteLine($"Email: {newCustomer.Email}");
        }

        public void CreateAccount(Customer customer, AccountType accountype)
        {          
             Account newAccount = null;

            string acctno = Account.GenerateAccountNumber();

            if (customer != null )
            {
                if (accountype == AccountType.Current)
                {
                    newAccount = new Current(acctno);
                
                }
                else
                {
                    newAccount = new Saving(acctno);
                }
            }

            Accounts.Add(newAccount);
            Console.WriteLine($"Account created successfully! Account Number: {newAccount.AccountNumber}, Account Type: {newAccount.AccountType}, Initial Balance: {newAccount.Balance:C}");                              
        }

        public Customer Login(string email, string password) 
        {
            foreach (var customer in CustomerList)
            {
                if (customer.Authenticate(email, password)) 
                {
                    Console.WriteLine("Login Succesful");
                        return customer;
                }
            }
            Console.WriteLine("Invalid email or password.");
            return null;
        }

    }
}
