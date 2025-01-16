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

        /*public static void PressExitToContinue()
        {
            Environment.Exit(0);
        }*/
    }
}
