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
using HireMockup.Validation;

namespace HireMockup
{
    /// <summary>
    /// Interaction logic for CreateCustomerWindow.xaml
    /// </summary>
    public partial class CreateCustomerWindow : Window
    {
        public CreateCustomerWindow()
        {
            InitializeComponent();
        }

        private void bttn_cancelOperation_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void bttn_submitCusomter_Click(object sender, RoutedEventArgs e)
        {
            string customerName = tbx_customerForename.Text.ToString();
            string customerSurname = tbx_customerSurname.Text.ToString();
            string customerAddress1 = tbx_customerAddress1.Text.ToString();
            string customerAddress2 = tbx_customerAddress2.Text.ToString();
            string email  = tbx_customerEmailAddress.Text.ToString();

            Boolean emailIsOk = Validation.EmailValidation.ValidateEmail(email);

            if(emailIsOk == true)
            {
                DataAccessLayer.newCustomer(customerName, customerSurname, customerAddress1, customerAddress2, email);
                DialogResult = false;
            }
            else
            {
                MessageBox.Show("Cannot create customer as the email provided is not in a valid format.");
            }

            
            
        }
    }
}
