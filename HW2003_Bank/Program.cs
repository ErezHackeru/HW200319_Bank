using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2003_Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer c1 = new Customer(1, "Reuven", 111);
            Customer c2 = new Customer(2, "Shimon", 112);
            Customer c3 = new Customer(3, "Levi", 113);
            Customer c4 = new Customer(4, "Yehuda", 114);

            Account a1 = new Account(c1, 12000);
            Account a2 = new Account(c2, 14000);
            Account a3 = new Account(c3, 16000);
            Account a4 = new Account(c4, 18000);

            Bank bank = new Bank();
            bank.AddNewCustomer(c1);
            bank.AddNewCustomer(c2);
            bank.AddNewCustomer(c3);
            bank.AddNewCustomer(c4);

            bank.OpenNewAccount(a1,c1);
            bank.OpenNewAccount(a2,c2);

            bank.OpenNewAccount(a3, c3);
            bank.OpenNewAccount(a4, c4);

            Customer GEtCustomerByID = bank.GetCustomerById(1);
            Customer GEtCustomerByNumber = bank.GetCustomerByNumber(1);
            Account GetAccountByNumber = bank.GetAccountByNumber(118);
            List<Account> GetAccountByCustomer = bank.GetAccountByCustomer(c1);
                        
            Console.ReadKey();
        }
    }
}
