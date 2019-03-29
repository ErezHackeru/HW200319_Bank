using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HW2003_Bank
{
    class BankDataToSave
    {
        public List<Account> accounts = new List<Account>();
        public List<Customer> customers = new List<Customer>();

        public Dictionary<int, Customer> customersAsCustomerID = new Dictionary<int, Customer>();
        public Dictionary<int, Customer> customersAsCustomerNumber = new Dictionary<int, Customer>();
        public Dictionary<int, Account> accountAsAccountNumber = new Dictionary<int, Account>();
        public Dictionary<Customer, List<Account>> accountAsAccountOwner = new Dictionary<Customer, List<Account>>();

        public double TotalMoneyInBank;
        public double profits;

        public string Name { get; set; }

        public string Address { get; set; }

        public int CustomerCount { get; set; }

        public BankDataToSave()
        {

        }

        //===========================
        //Static Part of the Class
        //===========================
        public static void SerializeABank(string fileName, BankDataToSave bank)
        {
            XmlSerializer myXmlSerializer = new XmlSerializer(typeof(BankDataToSave));
            using (Stream file = new FileStream(fileName, FileMode.Create))
            {
                myXmlSerializer.Serialize(file, bank);
            }
        }
        
        public static BankDataToSave DeserializeABank(string fileName)
        {
            BankDataToSave bank2;
            XmlSerializer myXmlSerializer = new XmlSerializer(typeof(BankDataToSave));
            using (Stream file = new FileStream(fileName, FileMode.Open))
            {
                bank2 = myXmlSerializer.Deserialize(file) as BankDataToSave;
            }
            return bank2;
        }
        
    }
}
