using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireMockup
{
    class Customer
    {
        // Properties of customer class
        public int customerID { get; set; }
        public string customerName { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public double customerBalance { get; set; }

        // tostring method to show in the ListBox

        public override string ToString()
        {
            return customerID + " - " + customerName + " - " + addressLine1 + " - " + addressLine2 + " - Balance - " + customerBalance;
        }


    }
}
