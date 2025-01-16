using BankManagementSystem.Services;
using BankManagementSystem.UI;
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
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string FullName {  get; set; } 
        public string Email {  get; set; }
        public string Password { get; set; }

        public Customer()
        {
            
        }

        public Customer(string firstName, string lastName, string email, string password)
        {           
            FullName = $"{firstName} {lastName}"; ;
            Email = email;
            Password = password;
        }           

      



            /*foreach (var item in accountService.AccountList)
            {
                if (customer.FullName == item.AccountName)
                {
                    Console.WriteLine($"\t\t\t\t\t\t{item.AccountType} - {item.AccountNumber} - Balance: {item.Balance}\n");
                }
                else
                {
                    Console.WriteLine($"\t\t\t\t\t- No account Yet");
                }
            }*/
        
    }
}
