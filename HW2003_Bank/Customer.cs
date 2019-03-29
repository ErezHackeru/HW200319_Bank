using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2003_Bank
{
    public class Customer
    {
        static int numberOfCust = 0;
        readonly int customerID;
        readonly int customerNumber;
        
        public string Name { get; private set; }
        public int PhNumber { get; private set; }
        public int CustomerID
        {
            get
            {
                return customerID;
            }
        }
        public int CustomerNumber
        {
            get
            {
                return customerNumber;
            }
        }
        public Customer()
        {

        }
        public Customer(int customerID, string name, int phNumber)
        {
            this.customerID = customerID;
            this.customerNumber = numberOfCust++;
            Name = name;
            PhNumber = phNumber;
        }

        public static bool operator ==(Customer c1 ,Customer c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null))
            {
                return true;
            }
            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null))
            {
                return false;
            }
            if (c1.customerNumber == c2.customerNumber)
                return true;
            return false;
        }

        public static bool operator !=(Customer c1, Customer c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object obj)
        {
            Customer otherCustomer = obj as Customer;
            if (otherCustomer == null)
                return false;
            return (otherCustomer.customerNumber == this.customerNumber);
        }

        public override int GetHashCode()
        {
            return this.customerNumber;
        }
    }
}
