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
        //Create a connection to the DB
        Model1Container db = new Model1Container();
        

        public MainWindow()
        {
            InitializeComponent();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from ha in db.HireAssets
                        select ha;

            var hireAssets = query.ToList();

            dataGrid_Home.ItemsSource = hireAssets;

        }

        private void bttn_listCustomers_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        select c;

            var customers = query.ToList();

            dataGrid_Home.ItemsSource = customers;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //]
            //    catch
            //    {
            //        MessageBox.Show("Invalid value for daily rate - must be decimal form e.g. 250.50");
            //    }
            //    HireItem FormHireItem = new HireItem()
            //    {
            //        HireID = txtbox_newHireID.Text,
            //        HireName = txtbox_newHireName.Text,
            //        DailyRate = dailyRate                   
                                        
            //    };
            //    HireList.Add(FormHireItem);
            //    lbx_homePage.Items.Refresh();
            //    tbctrl_main.SelectedIndex = 0;

            //}
        }

        private void btn_customerSearch_Click(object sender, RoutedEventArgs e)
        {
            string valueToSearch = customerSearchBox.Text.ToString();
            customerSearchMethod(valueToSearch);

        }

        public void customerSearchMethod(string valueToSearch)
        {
            Console.WriteLine(valueToSearch);

            var query = from c in db.Customers
                        where c.customerName == valueToSearch
                        select c;

            var results = query.ToList();

            dataGrid_Home.ItemsSource = results;
        }

        private void btn_hireSearch_Click(object sender, RoutedEventArgs e)
        {
            string valueToSearch = hireSearchBox.Text.ToString();
            Console.WriteLine($"Searching for {valueToSearch}");
            hireSearchMethod(valueToSearch);
        }

        public void hireSearchMethod(string valueToSearch)
        {
            Console.WriteLine(valueToSearch);

            var query = from ha in db.HireAssets
                        where ha.hireType == valueToSearch
                        select ha;

            var results = query.ToList();

            dataGrid_Home.ItemsSource = results;
        }
    }
}
