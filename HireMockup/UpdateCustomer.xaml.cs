using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HireMockup.DAL;


namespace HireMockup
{
    /// <summary>
    /// Interaction logic for UpdateCustomer.xaml
    /// </summary>
    public partial class UpdateCustomer : Window
    {
        public int customerID;
        public UpdateCustomer(Customer customer)
        {
          
            InitializeComponent();
            customerID = customer.Id;
            tbx_customerForename.Text = customer.customerName.ToString();
            tbx_customerSurname.Text = customer.customerSurname.ToString();
            tbx_customerAddress1.Text = customer.addressLine1.ToString();
            tbx_customerAddress2.Text = customer.addressLine2.ToString();
            tbx_customerEmailAddress.Text = customer.emailAddress.ToString();
            
        }

        private void bttn_updateCustomer_Click(object sender, RoutedEventArgs e)
        {

            

            string forename, surname, address1, address2, email;
            forename = tbx_customerForename.Text.ToString();
            surname = tbx_customerSurname.Text.ToString();
            address1 = tbx_customerAddress1.Text.ToString();
            address2 = tbx_customerAddress2.Text.ToString();
            email = tbx_customerEmailAddress.Text.ToString();

            Customer updateCustomer = new Customer
            {
                Id = customerID,
                customerName = forename,
                customerSurname = surname,
                addressLine1 = address1,
                addressLine2 = address2,
                emailAddress = email
            };

            CustomerDataAccess.updateCustomer(updateCustomer, forename, surname, address1, address2, email);

            MessageBox.Show("Customer Updated");

            

            DialogResult = false;

            
        }
        private void bttn_cancelOperation_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
