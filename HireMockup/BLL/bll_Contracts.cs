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

        public int contractID { get; set; }
        public decimal contractValue { get; set; }
        public Customer customer { get; set; }
        public HireAsset item { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return contractID.ToString() + " - " + contractValue.ToString("C") + " - " + item.hireName + " - " + customer.customerName + ' ' + customer.customerSurname;
        }

        // Returns a list of random contracts
        public static List<Contracts> ListRandomContracts()
        {
            Random rand = new Random();
            var customerList = DataAccessLayer.GetCustomerList();
            var hireList = DataAccessLayer.GetHireList();
            List<Contracts> contractList = new List<Contracts>();

            for (int i = 0; i < 25; i++)
            {
                int rContractID = rand.Next(1, 500);
                decimal rContractValue = rand.Next(1, 500);
                int index = rand.Next(customerList.Count);
                int index2 = rand.Next(hireList.Count);
                Customer rCustomer = customerList[index];
                HireAsset rHireAsset = hireList[index2];

                Contracts contract = new Contracts()
                {
                    contractID = rContractID,
                    contractValue = rContractValue,
                    customer = rCustomer,
                    item = rHireAsset

                };

                contractList.Add(contract);
            }
            return contractList;
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
