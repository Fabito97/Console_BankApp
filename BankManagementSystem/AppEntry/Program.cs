using BankManagementSystem.AppModels;
using BankManagementSystem.Enums;
using BankManagementSystem.Services;
using BankManagementSystem.UI;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.Unicode;
using System.Transactions;

namespace BankManagementSystem.AppEntry
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            bool isValidInput = true;

            do
            {
                Console.Title = "My Bank App";
                Console.WriteLine("----------------WELCOME TO MY BANK APP---------------");
                Console.WriteLine("\nWHAT WOULD YOU LIKE TO DO TODAY");
                Console.WriteLine("\n1. REGISTER");
                Console.WriteLine("2. LOGIN");
                Console.WriteLine("\n------------------------------------------------------\n");

                string input = Utitily.GetUserInput("an option to proceed");
                Console.WriteLine();

                if (input == "1")
                {
                    CustomerAuthService.Register();

                    Console.WriteLine("------------------LOGIN------------------\n");
                    CustomerAuthService.LoginCustomer();
                    
                }
                else if (input == "2")
                {
                    CustomerAuthService.LoginCustomer();                   
                }
                else
                {
                    Utitily.PrintMessage("Invalid input", false);
                    Utitily.PressEnterToContinue();
                    Console.Clear();

                    isValidInput = false;
                }
            } while (!isValidInput);


        } 
        
    }
}

