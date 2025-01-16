using BankManagementSystem.Data;
using BankManagementSystem.UI;
using BankManagementSystem.Validators;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankManagementSystem.Services
{
    public class CustomerAuthService
    {
        public static Customer LoggedInCustomer { get; private set; } = null;

        public CustomerAuthService()
        {

        }

        public static void Register()
        {
            var customers = CustomerService.GetCustomers();

            string firstName = Utitily.GetUserInput("Your First Name");
            firstName = Validator.ValidateName(firstName, "First Name");

            string lastName = Utitily.GetUserInput("Your last name");
            lastName = Validator.ValidateName(lastName, "Last Name");

            string email = Utitily.GetUserInput("Your email");
            email = Validator.ValidateEmail(email, customers);

            string password = Utitily.GetUserInput("Your password");
            password = Validator.ValidatePassword(password);

            Console.Clear();

            var existingCustomer = customers.FirstOrDefault(x => x.Email == email);

            if (existingCustomer != null)
            {
                Utitily.PrintMessage("A customer with this email already exists");
                return;

            }

            var newCustomer = new Customer(firstName, lastName, email, password);

            new CustomerService(FilePath.Customer).AddCustomerToFile(newCustomer);

            Console.WriteLine($"Full Name: {firstName} {lastName}");
            Console.WriteLine($"Email: {email}");                        
             
            Utitily.PressEnterToContinue();
            Console.Clear();
        }

        public static void LoginCustomer()
        {
            var customers = CustomerService.GetCustomers();           

            var verifiedCustomer = AuthenticateUser(customers);

            if (verifiedCustomer == null)
            {
                return;
            }
          
            LoggedInCustomer = verifiedCustomer;

            Console.Clear();
            Utitily.PrintMessage("Login Successful\n", true);
            Console.WriteLine($"Welcome, {LoggedInCustomer.FullName}\n");
            Utitily.PressEnterToContinue();
            Console.Clear();

            var customerAccounts = AccountService.GetAccounts().FindAll
               (x => x.CustomerId == LoggedInCustomer.Id);

            CheckForCustomerAccount(customerAccounts, LoggedInCustomer);

            if (customerAccounts.Count == 0) return;

            Console.WriteLine("Welcome to your Dashboard....");

            Thread.Sleep(1000);

            Console.Clear();

            AppScreen.CustomerDashboard(customerAccounts, LoggedInCustomer);
           

        }

        public static Customer AuthenticateUser(List<Customer> customers)
        {

            int maxAttempt = 3;

            while (maxAttempt > 0)
            {
                string email = Utitily.GetUserInput("your email");
                string password = Utitily.GetUserInput("your password");

                var customer = customers.FirstOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Password == password);

                if (customer != null)
                {
                    return customer;

                }

                maxAttempt--;

                Utitily.PrintMessage($"\nInvalid email or password. You have {maxAttempt} remaining attemps", false);
                Console.WriteLine();

                continue;                               

            }
            Console.WriteLine("Too many failed attempts. Please try again later.");
            return null;

        }

        private static void CheckForCustomerAccount(List<Account> customerAccounts, Customer loggedInCustomer)
        {
            if (customerAccounts.Count == 0)
            {
                Console.WriteLine("You currently have no account, please create an account to continue....");

                Utitily.PressEnterToContinue();
                Console.Clear();

                AccountService.CreateAccount(customerAccounts, loggedInCustomer);
            }
        }

        public static void Logout()
        {

            Utitily.PressEnterToContinue();

            LoggedInCustomer = null;

            Console.WriteLine("\n----------------Thank you for banking with us-------------\n");

            Thread.Sleep(1000);

            Environment.Exit(0);


        }

    }
}
