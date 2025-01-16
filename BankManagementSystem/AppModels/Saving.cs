using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BankManagementSystem.AppModels;
using BankManagementSystem.Data;
using BankManagementSystem.Enums;
using BankManagementSystem.Services;
using BankManagementSystem.UI;
using BankManagementSystem.Validators;
using Newtonsoft.Json;

namespace BankManagementSystem
{
    public class Saving : Account
    {


        public Saving() : base()
        {
            
        }

        public Saving(Guid customerId, string accountName, string accountNumber, AccountType accountType, decimal initialBalance) : 
            base(customerId, accountName, accountNumber, AccountType.Saving, initialBalance) 
        {
           
        }


    }
}

