using BankManagementSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.UI
{
    public class Utitily
    {
         public static string GetSecretInput(string prompt)
       {
            Console.Write(prompt);
            StringBuilder input = new StringBuilder();

            while (true)
            {                
                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (inputKey.Key != ConsoleKey.Backspace && inputKey.Key != ConsoleKey.Enter)
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write("*");
                }
                else if (inputKey.Key == ConsoleKey.Enter)
                {
                    if (input.Length >= 6)
                    {
                        break;
                    }
                    else
                    {
                        PrintMessage("\nPlease enter 6 digits or more.");
                        input.Clear();
                        Console.WriteLine();
                        continue;
                    }
                }                      
            }
            return input.ToString();
       }

        public static void PrintMessage(string message, bool success = true)
        {
            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(message);

            
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static string GetUserInput(string prompt)
        {
            Console.Write($"Enter {prompt}: ");

            return Console.ReadLine();
        }

        public static void PressEnterToContinue()
        {
            Console.WriteLine("\nPress enter if you wish to continue...");

            Console.ReadLine();
        
        }

		public static Account GetAccountSelectionType(string customerName, Guid customerId)
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

				PrintMessage("Invalid input, please try again", false);

				PressEnterToContinue();

			}

		}

		public static decimal ConvertAmountInput()
		{
			string amount;

			while (true)
			{
				amount = Utitily.GetUserInput("the amount you wish to deposit");
				Console.WriteLine();

				if (int.TryParse(amount, out int ValidAmount))
				{
					break;
				}
				else
				{
					Utitily.PrintMessage("Invalld input, please enter a valid amount\n", false);
				}
			}

			return Convert.ToDecimal(amount);
		}

		public static string GetSelectedAccountNumber(List<Account> customerAccounts)
		{

			if (customerAccounts.Count == 1)
			{
				return customerAccounts[0].AccountNumber;
			}

			int selectedOption;

			do
			{
				Console.WriteLine("---------SELECT AN ACCOUNT---------\n");

				int n = customerAccounts.Count;

				for (int i = 0; i < n; i++)
				{
					Console.WriteLine($"{i + 1}. {customerAccounts[i].AccountType} - {customerAccounts[i].AccountNumber}");
				}
				Console.WriteLine();

				var input = Utitily.GetUserInput("an option to proceed");

				if (int.TryParse(input, out selectedOption) && selectedOption >= 1 && selectedOption <= n)
				{
					break;
				}

				Utitily.PrintMessage("\nInvalid option, please try again", false);

				Utitily.PressEnterToContinue();
				Console.Clear();

			} while (true);

			return customerAccounts[selectedOption - 1].AccountNumber;

		}

	}
}
