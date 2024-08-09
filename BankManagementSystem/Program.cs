using BankManagementSystem.Services;
using BankManagementSystem.UI;

namespace BankManagementSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            var cus = new Customer();

            Console.Write("Enter First Name:");
            string firstName = cus.SetFirstName(Console.ReadLine());

            Console.Write("Enter last Name:");
            string lastName = cus.SetLastName(Console.ReadLine());

            Console.Write("Enter email:");
            string email = cus.SetEmail(Console.ReadLine());

            Console.Write("Enter password:");
            string password = cus.SetPassword(Console.ReadLine());




            /*AppScreen.Welcome();
            AppScreen.RegistrationForm();
            

            var cus = new Customer(1, "Fabian", "Muoghalu", "fabbenco97@gmail.com", "eajfj3");

            var acctno = BankService.GenerateAccountNumber();

            Account account = new Saving("1234567890", cus);            

            account.Deposit(1090);           
            account.Withdraw(90);

            account.GetBalance();

            Account current = new Current(BankService.GenerateAccountNumber(), cus);
            current.Deposit(2000);
            current.Withdraw(900);
            current.Transfer("1234567890", 100);

            Console.WriteLine("\nACCOUNT DETAILS");
            Console.WriteLine("+-------------------------------------------------------------------+");
            Console.WriteLine("|   FULL NAME\t  | ACCOUNT NUMBER | ACCOUNT TYPE | ACCOUNT BALANCE |");
            Console.WriteLine("+-------------------------------------------------------------------+");
            account.GetAccountDetails();
            current.GetAccountDetails();      
*/

            /* var acctNo = Account
             Account account1 = new Saving();            

             account1.Deposit(10900);           
             account1.Withdraw(900);

             account.GetBalance();

             Account current1 = new Current("Dave Muoghalu");
             current1.Deposit(20000, DateTime.Now, "Rent Payment");
             current1.Withdraw(9000, DateTime.Now, "Money for food");

             Console.WriteLine("\nACCOUNT DETAILS");
             Console.WriteLine("+-------------------------------------------------------------------+");
             Console.WriteLine("|   FULL NAME\t  | ACCOUNT NUMBER | ACCOUNT TYPE | ACCOUNT BALANCE |");
             Console.WriteLine("+-------------------------------------------------------------------+");
             account1.GetAccountDetails();
             current1.GetAccountDetails();*/
            // Console.WriteLine(account.Balance);
            /*AppScreen.Welcome();
            string email = Validator.Convert<string>("Your name");
            Console.WriteLine($"Your name is {email}");

            Utility.PressEnterToContinue();
            
*/

        }
       

        public void RegisterCustomer()
        {
            
        }
    }
}
