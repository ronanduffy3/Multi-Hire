using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireMockup.BLL
{
    public class bll_Customer
    {
        #region properties

        public int id { get; set; }
        public string customerName { get; set; }
        public string customerSurname { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public decimal accountBalance { get; set; }

        #endregion

        #region methods

        public override string ToString()
        {
            return id + " - " + customerName + " - " + customerSurname;
        }

        #endregion
    }
}
