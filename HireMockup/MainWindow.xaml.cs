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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HireMockup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //List<Author> AuthorList = new List<Author>();
        List<HireItem> HireList = new List<HireItem>();
        List<Customer> CustomerList = new List<Customer>();

        public MainWindow()
        {
            InitializeComponent();

            HireItem item1 = new HireItem()
            {
                HireID = "C105X",
                HireName = "Z60 Boom Lift",
                DailyRate = 250
            };
            HireItem item2 = new HireItem()
            {
                HireID = "C106X",
                HireName = "Z52 Boom Lift",
                DailyRate = 180
            };
            HireItem item3 = new HireItem()
            {
                HireID = "C205X",
                HireName = "Genie 32ft Scissor Lift",
                DailyRate = 150
            };

            Customer customer1 = new Customer()
            {
                customerID = 1,
                customerName = "Ronan Duffy",
                customerBalance = 0,
                addressLine1 = "Coill Darach",
                addressLine2 = "Castleblayney"
            };

            Customer customer2 = new Customer()
            {
                customerID = 2,
                customerName = "Eugene Callan",
                customerBalance = 0,
                addressLine1 = "Toome",
                addressLine2 = "Aghabog"
            };
            Customer customer3 = new Customer()
            {
                customerID = 3,
                customerName = "Mark Tavey",
                customerBalance = 0,
                addressLine1 = "Blackhill",
                addressLine2 = "Castleblayney"
            };

            HireList.Add(item1);
            HireList.Add(item2);
            HireList.Add(item3);

            CustomerList.Add(customer1);
            CustomerList.Add(customer2);
            CustomerList.Add(customer3);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lbx_homePage.ItemsSource = HireList;

        }

        private void bttn_listCustomers_Click(object sender, RoutedEventArgs e)
        {
            lbx_homePage.ItemsSource = CustomerList;
        }
    }
}
