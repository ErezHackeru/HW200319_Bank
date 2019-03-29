using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2003_Bank
{
    public class Bank : IBank
    {
        List<Account> accounts = new List<Account>();
        List<Customer> customers = new List<Customer>();

        Dictionary<int, Customer> customersAsCustomerID = new Dictionary<int, Customer>();
        Dictionary<int, Customer> customersAsCustomerNumber = new Dictionary<int, Customer>();
        Dictionary<int, Account> accountAsAccountNumber = new Dictionary<int, Account>();
        Dictionary<Customer, List<Account>> accountAsAccountOwner = new Dictionary<Customer, List<Account>>();

        private double TotalMoneyInBank;
        private double profits;

        public string Name { get; private set; }

        public string Address { get; private set; }

        public int CustomerCount { get; private set; }

        public Bank()
        {

        }
        public Customer GetCustomerById(int customerID)
        {
            if (customersAsCustomerID.ContainsKey(customerID))
                return customersAsCustomerID[customerID];

            throw new CustomerNotFoundException($"Customer ID { customerID } was not found");

            //return customersAsCustomerID[customerID];
        }
        public Customer GetCustomerByNumber(int customerNumber)
        {
            if (!customersAsCustomerNumber.ContainsKey(customerNumber))
                throw new CustomerNotFoundException($"Customer Number { customerNumber } was not found");

            return customersAsCustomerNumber[customerNumber];
        }
        public Account GetAccountByNumber(int accountNumber)
        {
            if (!accountAsAccountNumber.ContainsKey(accountNumber))
                throw new AccountNotFoundException($" Account number { accountNumber } was not found");

            return accountAsAccountNumber[accountNumber];
        }
        public List<Account> GetAccountByCustomer(Customer customer)
        {
            if (!accountAsAccountOwner.ContainsKey(customer))
                throw new AccountNotFoundException($"{ customer.Name } Account was not found");
            
            return accountAsAccountOwner[customer];
        }
        public void AddNewCustomer(Customer customer)
        {
            if (customers.Contains(customer))
                throw new CustomerAlreadyExistException($"{customer.Name} already exist");
            else
            {
                customers.Add(customer);
                customersAsCustomerID.Add(customer.CustomerID, customer);
                customersAsCustomerNumber.Add(customer.CustomerNumber, customer);

                CustomerCount++;
            }
        }

        public void OpenNewAccount(Account account, Customer customer)
        {           
            if (accounts.Contains(account))            
                throw new AccountAlreadyExistException($"Account number {account.AccountNumber} and customer {customer.Name} already exist");            
            else
            {
                accounts.Add(account);
                accountAsAccountNumber.Add(account.AccountNumber, account);
                List<Account> NewAccounts = new List<Account>();
                NewAccounts.Add(account);
                accountAsAccountOwner.Add(customer, NewAccounts);
            }                
        }

        public double Deposit(Account account, double amount)
        {
            account.Add(amount);
            TotalMoneyInBank += amount;
            return TotalMoneyInBank;
        }
        public double Withdraw(Account account, double amount)
        {
            if (TotalMoneyInBank - amount > account.MaxMinusAllowed)
            {
                account.Subtract(amount);
                TotalMoneyInBank -= amount;
            }
            else
                throw new BalanceEception();
            
            return TotalMoneyInBank;
        }
        public double GetCustomerTotalBalance(Account account)
        {
            return account.Balance;
        }
        public void CloseAccount(Account account, Customer customer)
        {
            if (customers.Contains(customer))
                customers.Remove(customer);
            if (accounts.Contains(account))
                accounts.Remove(account);
        }
        public void ChargeAnnualCommision(float percentage)
        {
            double commision = 0;
            foreach (Account account in accounts)
            {
                commision = account.Balance * percentage;
                account.Subtract(commision);
                profits += commision;
            }
        }
        public void JoinAccounts(Account account1, Account account2)
        {
            Account NewAccount = account1 + account2;
            Customer OldCustomer = null;
            List<Account> AllAccount1 = new List<Account>();
            List<Account> AllAccount2 = new List<Account>();
            foreach (KeyValuePair<Customer,List<Account>> AccountAsAccountOwner in accountAsAccountOwner)
            {
                if ((AccountAsAccountOwner.Value.Contains(account1)) && (AccountAsAccountOwner.Value.Contains(account2)))
                    OldCustomer = AccountAsAccountOwner.Key;                
            }
            if (OldCustomer == null)
                throw new NotSameCustomerException();

            accounts.Remove(account1);
            accounts.Remove(account2);
            accounts.Add(NewAccount);
            customers.Remove(OldCustomer);
            CloseAccount(account1, OldCustomer);

            Customer NewCustomer = new Customer(NewAccount.AccountOwner.CustomerID, 
                NewAccount.AccountOwner.Name, NewAccount.AccountOwner.PhNumber);
            customers.Add(NewCustomer);
        }

        public static void SaveDataToXML(Bank bank)
        {
            string fileNameBank = @"d:\temppp\BankXml.xml";
            BankDataToSave bankDataToSave = new BankDataToSave()
            {
                accounts = bank.accounts,
                customers = bank.customers,
                customersAsCustomerID = bank.customersAsCustomerID,
                customersAsCustomerNumber = bank.customersAsCustomerNumber,
                accountAsAccountNumber = bank.accountAsAccountNumber,
                accountAsAccountOwner = bank.accountAsAccountOwner,

                TotalMoneyInBank = bank.TotalMoneyInBank,
                profits = bank.profits,
                Name = bank.Name,
                Address = bank.Address,
                CustomerCount = bank.CustomerCount
            };

            BankDataToSave.SerializeABank(fileNameBank, bankDataToSave);
        }

        public static Bank LoadDataFromXML(string fileName)
        {
            BankDataToSave bankDataToSave = BankDataToSave.DeserializeABank(fileName);
            return new Bank()
            {
                accounts = bankDataToSave.accounts,
                customers = bankDataToSave.customers,
                customersAsCustomerID = bankDataToSave.customersAsCustomerID,
                customersAsCustomerNumber = bankDataToSave.customersAsCustomerNumber,
                accountAsAccountNumber = bankDataToSave.accountAsAccountNumber,
                accountAsAccountOwner = bankDataToSave.accountAsAccountOwner,

                TotalMoneyInBank = bankDataToSave.TotalMoneyInBank,
                profits = bankDataToSave.profits,
                Name = bankDataToSave.Name,
                Address = bankDataToSave.Address,
                CustomerCount = bankDataToSave.CustomerCount
            };            
        }
    }
}
