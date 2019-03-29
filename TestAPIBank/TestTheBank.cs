using System;
using System.Collections.Generic;
using HW2003_Bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestAPIBank
{
    [TestClass]
    public class TestTheBank
    {
        static readonly Customer c1 = new Customer(1, "Reuven", 111);
        static readonly Customer c2 = new Customer(2, "Shimon", 112);
        static readonly Customer c3 = new Customer(3, "Levi", 113);
        static readonly Customer c4 = new Customer(4, "Yehuda", 114);

        Account a1 = new Account(c1, 12000);
        Account a2 = new Account(c2, 14000);
        Account a3 = new Account(c3, 16000);
        Account a4 = new Account(c4, 18000);

        static readonly Bank bank = new Bank();

        [TestMethod]
        [ExpectedException(typeof(CustomerAlreadyExistException))]
        public void AddNewCustomerTest()//method 1
        {
            InitializeBank();

            bank.AddNewCustomer(c1);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void GetCustomerByIdTest()//method 2
        {
            Customer GEtCustomerByID_2 = bank.GetCustomerById(50);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void GetCustomerByNumberTest()//method 3
        {
            Customer GEtCustomerByNumber_2 = bank.GetCustomerByNumber(118);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void GetAccountByNumberTest()//method 4
        {
            Account GetAccountByNumber_2 = bank.GetAccountByNumber(118);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void GetAccountByCustomerTest()//method 5
        {
            List<Account> GetAccountByCustomer_2 = bank.GetAccountByCustomer(new Customer(26, "Moshe", 557));
        }

        [TestMethod]
        [ExpectedException(typeof(AccountAlreadyExistException))]
        public void OpenNewAccountTest()//method 6
        {
            Customer cu1 = new Customer(100, "Example", 221);
            Account ac1 = new Account(c1, 22100);
            bank.OpenNewAccount(ac1, cu1);
            bank.OpenNewAccount(ac1, cu1);
        }

        [TestMethod]
        [ExpectedException(typeof(BalanceEception))]
        public void WithdrawTest()//method 7
        {
            double x = bank.Withdraw(a2, (a2.Balance + 10));
        }

        [TestMethod]        
        public void DipositTest()//method 8
        {
            double x = bank.Deposit(a2, (a2.Balance + 10));
        }

        [TestMethod]
        public void GetCustomerTotalBalanceTest()//method 8
        {
            double x = bank.GetCustomerTotalBalance(a2);
        }

        [TestMethod]
        public void CloseAccountTest()//method 8
        {
            Customer cu2 = new Customer(101, "Exampl", 222);
            Account ac2 = new Account(c1, 22100);
            bank.OpenNewAccount(ac2, cu2);
            
            bank.CloseAccount(ac2,cu2);
        }
        private void InitializeBank()
        {
            bank.AddNewCustomer(c1);
            bank.AddNewCustomer(c2);
            bank.AddNewCustomer(c3);
            bank.AddNewCustomer(c4);

            bank.OpenNewAccount(a1, c1);
            bank.OpenNewAccount(a2, c2);
            bank.OpenNewAccount(a3, c3);
            bank.OpenNewAccount(a4, c4);

            //Customer GEtCustomerByID = bank.GetCustomerById(1);
            //Customer GEtCustomerByNumber = bank.GetCustomerByNumber(1);
            //Account GetAccountByNumber = bank.GetAccountByNumber(118);
            //List<Account> GetAccountByCustomer = bank.GetAccountByCustomer(c1);

        }
    }
}
