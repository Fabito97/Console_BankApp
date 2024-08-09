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



        public Customer() { }
        public Customer(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Fullname = FirstName + " " + LastName;
            Password = string.Empty;
            Accounts = new List<Account>();            
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
            return Email = email;
        }

        private bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*[@#$%^&!]).{6,}$";
            return Regex.IsMatch(password, pattern);
        }

        public string SetPassword(string password)
        {            
            while(!IsValidPassword(password))
            {               
               Console.WriteLine("Password must be at least 6 characters long, contain an uppercase letter, and include a special character.");
               Console.WriteLine("\nEnter your first Name");
               password = Console.ReadLine();
            }
            return Password = password;
        }

       

        public bool Authenticate(string email, string password)
        {
            return Email.ToLower() == email.ToLower() && Password == password;
        }


       public void CreateAccount(Account account)
        {
            Accounts.Add(account);
        }         
    }
}
