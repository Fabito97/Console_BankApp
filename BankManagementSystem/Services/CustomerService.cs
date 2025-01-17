using BankManagementSystem.AppModels;
using BankManagementSystem.Data;
using BankManagementSystem.Enums;
using BankManagementSystem.UI;
using BankManagementSystem.Validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace BankManagementSystem.Services
{
    public class CustomerService
    {
        private readonly string _filePath;
        public CustomerService() { }

        public CustomerService(string path)
        {

            _filePath = path;

            if (!File.Exists(_filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
                File.WriteAllText(_filePath, "[]");
            }
        }


		public void AddCustomerToFile(Customer customer)
        {
            try
            {
                var customers = new List<Customer>();

                var jsonData = File.ReadAllText(_filePath);
                customers = JsonConvert.DeserializeObject<List<Customer>>(jsonData) ?? new List<Customer>();

                customers.Add(customer);

                try
                {
                    string updatedJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
                    File.WriteAllText(_filePath, updatedJson);

                    Utitily.PrintMessage("Customer registration successfully");

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        
        public static List<Customer> GetCustomers()
        {
            try
            {                            
                var customers = new List<Customer>();


                if (File.Exists(FilePath.Customer))  
                {
                    var jsonData = File.ReadAllText(FilePath.Customer);
                    customers = JsonConvert.DeserializeObject<List<Customer>>(jsonData) ?? new List<Customer>();
                }
                return customers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void PrintAllCustomers()
        {
            var customers = GetCustomers();

            if (customers == null)
            {

                Console.WriteLine("No customer yet");
            }

            else
            {
                Console.WriteLine("Name \t | Email \t | ");
                foreach (Customer customer in customers)
                {

                    Console.WriteLine($"{customer.FullName} | {customer.Email} ");
                }

            }

        }

    }
}
