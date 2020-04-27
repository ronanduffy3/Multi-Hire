using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HireMockup.DAL;
using Newtonsoft.Json;

namespace HireMockup.BLL
{
    public class Contracts
    {
        #region Properties

        // Define what a contract is made up of. An ID, Value, must be attributed to a customer and must also have an item.
        public int contractID { get; set; }
        public decimal contractValue { get; set; }
        public Customer customer { get; set; }
        public HireAsset item { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return contractID.ToString() + " - " + contractValue.ToString("C") + " - " + item.hireName + " - " + customer.customerName + " " + customer.customerSurname;
        }

        // Returns a list of random contracts
        public static List<Contracts> ListRandomContracts()
        { 
            Random rand = new Random();
            var customerList = DataAccessLayer.GetCustomerList();
            var hireList = DataAccessLayer.GetHireList();
            List<Contracts> contractList = new List<Contracts>();

            // Generates 25 random contracts but this could easily be 5 or 50
            for (int i = 0; i < 25; i++)
            {
                int rContractID = rand.Next(1, 500);
                decimal rContractValue = rand.Next(1, 500);
                // Create an index of the amount of customers/nireassets to create a random
                int index = rand.Next(customerList.Count);
                int index2 = rand.Next(hireList.Count);
                // Create two temp customers and hireasssets
                Customer rCustomer = customerList[index];
                HireAsset rHireAsset = hireList[index2];

                // Create the contracts object 
                Contracts contract = new Contracts()
                {
                    contractID = rContractID,
                    contractValue = rContractValue,
                    customer = rCustomer,
                    item = rHireAsset

                };

                // Add it to the list
                contractList.Add(contract);
            }
            // Return it for use in the listbox
            return contractList;
        }
        
        // Method to create a new contract based on a specific customer and hireAsset.
        public static Contracts addContract(Customer _customer, HireAsset hireAsset)
        {
            // Create an instance of random
            Random rand = new Random();

            Contracts newContract = new Contracts()
            {
                // Just create a random contract ID and value for show
                contractID = rand.Next(1, 500),
                contractValue = rand.Next(1,500),
                customer = _customer,
                item = hireAsset
            };
            // The method returns a type of Contracts so that can be added to the list we have alreadym
            return newContract;
        }

        // Method that writes the list of contracts to a txt file 
        public static void WriteContractsToFile(List<Contracts> contractList)
        {
            string contractData = JsonConvert.SerializeObject(contractList, Formatting.Indented);

            using(StreamWriter stream = new StreamWriter("c:/temp/contractdata.json"))
            {
                stream.Write(contractData);
                stream.Close();
            }
        }

        #endregion
    }


}
