using HireMockup.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HireMockup.DAL
{
    class CustomerDataAccess
    {
        /*
         * 
         *  HANDLES ALL CUSTOMER DATA QUERYS REQUIRED (UPDATE, CREATE, READ AND DELETE)
         * 
         */

        #region connection strings
        private Model1Container db = new Model1Container();
        #endregion

        #region reads
        // Method to read in all customers (datatable standard need to change to observable collectpion)
        public static DataTable AllCustomers()
        {
            // Method that returns a datatable of all customers.
            DataTable tbl = new DataTable();

            tbl.Columns.Add("Customer ID", typeof(int));
            tbl.Columns.Add("Customer Name", typeof(string));
            tbl.Columns.Add("Customer Surname", typeof(string));
            tbl.Columns.Add("Address Line 1", typeof(string));
            tbl.Columns.Add("Address Line 2", typeof(string));
            tbl.Columns.Add("Account Balance", typeof(decimal));
            tbl.Columns.Add("Customer Email", typeof(string));

            using (var context = new Model1Container())
            {
                var query = (from customer in context.Customers
                             select customer).ToList();

                // Loop over the customer objects in query and add each piece of data on a header in a row.
                foreach (var customer in query)
                {
                    tbl.Rows.Add(customer.Id, customer.customerName, customer.customerSurname, customer.addressLine1, customer.addressLine2, customer.accountBalance, customer.emailAddress);
                }

            }
            return tbl;
        }
        

        public static DataTable CustomerSearch(string custName, string custSurname)
        {
            // Pass in the values of the search box and use a workaround seen in mainwindow.xaml.cs to avoid error.
            DataTable tbl = new DataTable();

            tbl.Columns.Add("Customer ID", typeof(Int32));
            tbl.Columns.Add("Customer Name", typeof(string));
            tbl.Columns.Add("Customer Surname", typeof(string));
            tbl.Columns.Add("Address Line 1", typeof(string));
            tbl.Columns.Add("Address Line 2", typeof(string));
            tbl.Columns.Add("Account Balance", typeof(decimal));

            using (var context = new Model1Container())
            {
                // Return any customer that matches first or surname
                var query = (from customer in context.Customers
                             where customer.customerName.Equals(custName) || customer.customerSurname.Equals(custSurname)
                             select customer).ToList();
                // Similar to above loop over the found customers and insert each piece of data on a header in a row.
                foreach (var customer in query)
                {
                    tbl.Rows.Add(customer.Id, customer.customerName, customer.customerSurname, customer.addressLine1, customer.addressLine2, customer.accountBalance);
                }

            }
            return tbl;
        }
        

        // Returns a list of customers
        public static List<Customer> GetCustomerList()
        {
            List<Customer> customerList = new List<Customer>();
            using (var context = new Model1Container())
            {
                var query = (from c in context.Customers
                             select c).ToList();

                foreach (var c in query)
                {
                    customerList.Add(c);
                }
            }
            return customerList;
        }

        // Bill a customer from a contract
        public static void BillCustomer(List<Contracts> contractsList, int contractIDToBill)
        {
            decimal valueToBill = 0;
            var contractToBill = contractsList.FirstOrDefault(o => o.contractID == contractIDToBill);
            decimal.TryParse(contractToBill.contractValue.ToString(), out valueToBill);
            int customerIdToBill = int.Parse(contractToBill.customer.Id.ToString());

            using (var context = new Model1Container())
            {
                var query = from c in context.Customers
                            where c.Id == customerIdToBill
                            select c;

                foreach (var cust in query)
                {
                    if (cust.accountBalance >= 0)
                    {
                        cust.accountBalance += valueToBill;
                    }
                    else if (cust.accountBalance < 0)
                    {
                        cust.accountBalance -= valueToBill;
                    }
                }
                contractsList.Remove(contractToBill);
                context.SaveChanges();
            }
        }
        #endregion

        #region writes to database
        // Create a new customer
        [Obsolete]
        public static void newCustomer(string firstName, string lastName, string address1, string address2, string email)
        {
            decimal newAccountBalance = 0;
            try
            {
                SqlConnection sql = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\DATABASES\MULTIHIREDB.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                sql.Open();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Customers (customerName, customerSurname, addressLine1, addressLine2, accountBalance, emailAddress) VALUES (@customerName, @customerSurname, @addressLine1, @addressLine2, @accountBalance, @emailAddress)", sql);
                sqlCommand.Parameters.Add("@customerName", firstName);
                sqlCommand.Parameters.Add("@customerSurname", lastName);
                sqlCommand.Parameters.Add("@addressLine1", address1);
                sqlCommand.Parameters.Add("@addressLine2", address2);
                sqlCommand.Parameters.Add("@accountBalance", newAccountBalance);
                sqlCommand.Parameters.Add("@emailAddress", email);
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Following error has occured: {ex}");
            }
            finally
            {
                MessageBox.Show("Item successfully added");
            }
        }
        #endregion
    }
}
