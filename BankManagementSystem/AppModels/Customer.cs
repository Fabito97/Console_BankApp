using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankManagementSystem
{
    public class Customer
    {
        /*public string FirstName { get; set; } 
        public string LastName { get; set; }*/
        public int Id { get; set; } 
        public string FirstName {  get; set; } 
        public string LastName { get; set; }
        public string Fullname { get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }

        public List<Account> Accounts;

        private List<Customer> CustomerList;

        public Customer() 
        {
            Accounts = new List<Account>();
            CustomerList = new List<Customer>();
        }
        public Customer(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Fullname = FirstName + " " + LastName;
            Password = string.Empty;            
        }


        public string SetFirstName(string firstName)
        {            
            while (firstName == null || char.IsLower(firstName[0]) || char.IsNumber(firstName[0]))
            {                
                Console.WriteLine("First name must not begin with a lowercase letter or a number");
                Console.WriteLine("\nEnter your first Name");
                firstName = Console.ReadLine();
            }
            
           return FirstName = firstName;
        }

        public string SetLastName(string lastName)
        {            
            
            while (lastName == null || char.IsLower(lastName[0]) || char.IsNumber(lastName[0]))
            {                
                Console.WriteLine("First name should begin with an uppercase letter and it must not begin with a number");
                Console.WriteLine("\nEnter your first Name");
                lastName = Console.ReadLine();
            }
            return FirstName = lastName;
        }
                     

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("Customer cannot be null");
            }

            CustomerList.Add(customer);

        }

        public void CreateAccount(Account account)
        {
            Accounts.Add(account);

        }
             
        public List<Customer> GetCustomers()
        {
            return CustomerList;
        }

        public List<Account> GetAccounts()
        {
            return Accounts;
        }
    }
}
