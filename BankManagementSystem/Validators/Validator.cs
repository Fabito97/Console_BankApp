using BankManagementSystem.Services;
using BankManagementSystem.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankManagementSystem.Validators
{
    public class Validator
    {

        public static string ValidateName(string name, string fieldName)
        {
            while (name == string.Empty || char.IsLower(name[0]) || char.IsNumber(name[0]))
            {
                Utitily.PrintMessage($"\n{fieldName} must not begin with a lowercase letter or a number", false);

                name = Utitily.GetUserInput($"your {fieldName}");
            }
            return name;
        }

        public static string ValidateEmail(string email, List<Customer> customers)
        {
            string validPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            while (Regex.IsMatch(email, validPattern))
            {
                Utitily.PrintMessage("Invalid email format.\n", false);
                Console.Write("Enter your email.");
                email = Console.ReadLine();
            }

            bool emailExist = CheckIfEmailExist(email, customers);

            if (emailExist)
            {
                Utitily.PrintMessage("Email already exists, please proceed to login.\n", false);

                Utitily.PressEnterToContinue();
                Console.Clear();


                CustomerAuthService.LoginCustomer();
            }

            return email;
        }

        private static bool CheckIfEmailExist(string email, List<Customer> customers)
        {

            return customers.Any(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            
        }

        public static string ValidatePassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*[@#$%^&!]).{6,}$";

            while (Regex.IsMatch(password, pattern))
            {
                Utitily.PrintMessage("Password must be at least 6 characters long, \ncontain an uppercase letter, and include a special character.", false);

                Utitily.GetUserInput("your password ");
                password = Console.ReadLine();
            }
            return password;
        }

    
    }
}
