using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.UI
{
    public static class AppScreen
    {
        internal static void Welcome()
        {
            Console.Clear();
            Console.Title = "My Bank App";

            Console.WriteLine("\n--------------- Welcome to My Bank App -----------------");

            Console.WriteLine("\n1. Register");
            Console.WriteLine("2. Create a new account");
            Console.WriteLine("3. Login");

            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine("Please select an option to continue");
            Console.WriteLine("-------------------------------------------");
        }

       
        
    }
}
