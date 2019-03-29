using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2003_Bank
{
    public class Account
    {
        private static int numberOfAcc = 0;
        private readonly int accountNumber;
        private readonly Customer accountOwner;
        private int maxMinusAllowed;
        private int monthlyIncome;

        public int AccountNumber
        {
            get
            {
                return accountNumber;
            }
        }
        public double Balance { get; private set; }
        public Customer AccountOwner
        {
            get
            {
                return accountOwner;
            }
        }
        public int MaxMinusAllowed { get; }

        public Account(Customer accountOwner, int monthlyIncome)
        {
            this.accountNumber = numberOfAcc++;
            this.accountOwner = accountOwner;
            this.monthlyIncome = monthlyIncome;

            maxMinusAllowed = this.monthlyIncome * 3;
        }

        public void Add(double Amount)
        {
            this.Balance += Amount;
        }

        public void Subtract(int Amount)
        {
            this.Balance -= Amount;
        }

        public void Subtract(double Amount)
        {
            this.Balance -= Amount;
        }

        public static bool operator ==(Account c1, Account c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null))
            {
                return true;
            }
            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null))
            {
                return false;
            }
            if (c1.accountNumber == c2.accountNumber)
                return true;
            return false;
        }
        public static bool operator !=(Account c1, Account c2)
        {
            return !(c1 == c2);
        }
        public override bool Equals(object obj)
        {
            Account otherAcount = obj as Account;
            if (otherAcount == null)
                return false;
            return (otherAcount.accountNumber == this.accountNumber);
        }

        public override int GetHashCode()
        {
            return this.accountNumber;
        }

        public static Account operator +(Account a1, Account a2)
        {
            Customer NewCustomer = new Customer(a1.accountOwner.CustomerID + a2.accountOwner.CustomerID,
                $"{a1.accountOwner.Name} + {a2.accountOwner.Name}", 5006465);
            return new Account(NewCustomer, 10000);
        }

        public static Account operator +(Account a1 , double amount)
        {
            Account account = new Account(a1.accountOwner, a1.monthlyIncome);
            account.Add(amount);
            return account;
        }

        public static Account operator -(Account a1, double amount)
        {
            Account account = new Account(a1.accountOwner, a1.monthlyIncome);
            account.Subtract(amount);
            return account;
        }
    }
}
