using BankManagementSystem.Services;

namespace BankManagementSystem.AppEntry
{
    public class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();
           // Customer newcustomer = new Customer("Fabian", "Muoghalu", "fabbenco97@gmail.com", "Password@12");
            CustomerService customerService = new CustomerService();

           // customer.AddCustomer(newcustomer);

            customerService.Registercustomer("Fabian", "Muoghalu", "fabbenco97@gmail.com", "Password@12");

            customerService.CreateAccount("1290282651", customer.Fullname, Enums.AccountType.Saving);
            customerService.CreateAccount("1290284651", customer.Fullname, Enums.AccountType.Current);

            Account current = new Current("1290284651", customer.Fullname, Enums.AccountType.Current, 1000);
            current.Withdraw(2000);
            current.Deposit(2000);

           

            customerService.GetAccountDetails("Fabian Muoghalu");





                
 


        }

    }
}
