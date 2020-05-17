using HireMockup.BLL;
using HireMockup.DAL;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HireMockup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Contracts> contracts = new List<Contracts>();

        public ObservableCollection<Customer> custList = new ObservableCollection<Customer>(CustomerDataAccess.GetCustomerList());
        public ObservableCollection<HireAsset> assetList = new ObservableCollection<HireAsset>(DataAccessLayer.GetHireList());

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        // method to list all hire equipment
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable query_results = DataAccessLayer.ListHireAssets();
            dataGrid_Home.DataContext = query_results.DefaultView;
        }

        // Method to list all customers
        private void bttn_listCustomers_Click(object sender, RoutedEventArgs e)
        {
            DataTable query_results = CustomerDataAccess.AllCustomers();

            dataGrid_Home.DataContext = null;
            dataGrid_Home.DataContext = query_results.DefaultView;
        }

        // Customer Search Function
        private void btn_customerSearch_Click(object sender, RoutedEventArgs e)
        {
            // Split the search string to avoid errors
            string[] query_strings = customerSearchBox.Text.ToString().Split(' ');
            if (query_strings[1] != null)
            {
                DataTable query_results = CustomerDataAccess.CustomerSearch(query_strings[0], query_strings[1]);
                dataGrid_Home.DataContext = query_results.DefaultView;
            }
            else
            {
                query_strings[1] = " ";
                DataTable query_results = CustomerDataAccess.CustomerSearch(query_strings[0], query_strings[1]);
                dataGrid_Home.DataContext = query_results.DefaultView;
            }
        }

        // Hire Search Function
        private void btn_hireSearch_Click(object sender, RoutedEventArgs e)
        {
            // Get the required search value
            string valueToSearch = hireSearchBox.Text.ToString();

            // Run the search function from DAL
            DataTable query = DataAccessLayer.HireAssetSearch(valueToSearch);
            dataGrid_Home.DataContext = null;
            dataGrid_Home.DataContext = query.DefaultView;
        }

        // Popuate dataGrid on load
        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable query = DataAccessLayer.ListEmployee();
            cbx_salaryCalculator.ItemsSource = Enum.GetValues(typeof(SalaryCalculator.JobTitles));

            employeeDataGrid.DataContext = query.DefaultView;

            refreshSalarys();
        }

        // Remove Employee
        private void button_Click_2(object sender, RoutedEventArgs e)
        {
            int selectedEmployee = 0;
            try
            {
                int.TryParse(tbx_selectedEmployee.Text, out selectedEmployee);
            }
            catch (SystemException ex)
            {
                MessageBox.Show($"Invalid Input Exception: {ex}");
            }
            DataAccessLayer.RemoveEmployeeByID(selectedEmployee);

            // Refresh Code
            refreshSalarys();
            DataTable query = DataAccessLayer.ListEmployee();
            employeeDataGrid.DataContext = null;
            employeeDataGrid.DataContext = query.DefaultView;
        }

        // Populate the listbox on the third tab with random contracts
        public void listBox_Loaded(object sender, RoutedEventArgs e)
        {
            contracts = Contracts.ListRandomContracts();
            lbx_contracts.ItemsSource = contracts.ToList();
        }

        // Save all the contracts to file
        private void btn_saveToFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Contracts.WriteContractsToFile(contracts);
                MessageBox.Show("Contracts saved to c:/temp");
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Salary Calculator Event
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Decimal for weeks worked as you can work a week and a half (1.5)
            decimal weeksWorked;
            // Use the enum put then convert to string for use in a switch case as seen in salary calculator method.
            string position = cbx_salaryCalculator.SelectedItem.ToString();

            // Try catch to avoid a crash on invalid data
            try
            {
                weeksWorked = decimal.Parse(tbx_weeksWorked.Text);
                decimal salary = SalaryCalculator.CalculateSalary(position, weeksWorked);
                lbl_salaryCalculator.Content = salary.ToString("C");
            }
            catch (SystemException ex)
            {
                MessageBox.Show($"Input data invalid. System Exception: {ex}");
            }
        }

        // Bill a contract button
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // Initialize a temporary int to then convert the textbox to it
            int contractID;
            try
            {
                int.TryParse(tbx_ContractID.Text, out contractID);
                // Call the billcustomer method 
                CustomerDataAccess.BillCustomer(contracts, contractID);
                // Refresh list logic
                lbx_contracts.ItemsSource = null;
                lbx_contracts.ItemsSource = contracts.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Method to refresh the salary labels
        public void refreshSalarys()
        {
            // Quite beefy code but wasn't sure if there was another way to go about it will do. Works as intended anyways.
            lbl_wageTotal.Content = null;
            lbl_hireBill.Content = null;
            lbl_managerBill.Content = null;
            lbl_mechanicBill.Content = null;
            lbl_adminBill.Content = null;
            decimal salaryBill = DataAccessLayer.CalculateSalaryBill();
            decimal hireSalaryBill = DataAccessLayer.CalculateCertainSalary("Hire Operator");
            decimal managerSalaryBill = DataAccessLayer.CalculateCertainSalary("Manager");
            decimal mechanicSalaryBill = DataAccessLayer.CalculateCertainSalary("Mechanic");
            decimal adminSalaryBill = DataAccessLayer.CalculateCertainSalary("Admin");
            lbl_wageTotal.Content = salaryBill.ToString("C");
            lbl_hireBill.Content = hireSalaryBill.ToString("C");
            lbl_managerBill.Content = managerSalaryBill.ToString("C");
            lbl_mechanicBill.Content = mechanicSalaryBill.ToString("C");
            lbl_adminBill.Content = adminSalaryBill.ToString("C");
        }

        private void bttn_CreateContract_Click(object sender, RoutedEventArgs e)
        {
            // Cast the selected items in the combox to a customer object
            Customer customer = (Customer)comboBox_allCustomers.SelectedItem;
            HireAsset hireAsset = (HireAsset)comboBox_allHireAssets.SelectedItem;

            //  Create a temporary contract to add to the contracts list
            Contracts tempContract = BLL.Contracts.addContract(customer, hireAsset);
            // Add the temp contract to the list
            contracts.Add(tempContract);

            // Refresh logic for contracts listbox
            lbx_contracts.ItemsSource = null;
            lbx_contracts.ItemsSource = contracts.ToList();
        }

        // Populate combobox's with observables when the tabs open to avoid error.
        private void tbctrl_main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbctrl_main.SelectedIndex == 2)
            {
                comboBox_allCustomers.ItemsSource = custList;
                comboBox_allHireAssets.ItemsSource = assetList;
            }
            comboBox_allCustomers.DisplayMemberPath = "customerName";
            comboBox_allCustomers.SelectedValuePath = "Id";
            comboBox_allHireAssets.DisplayMemberPath = "hireName"; // Or whatever should be shown to the user in the combobox
            comboBox_allHireAssets.SelectedValuePath = "hireId";
        }

        private void bttn_addHireWindow_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CreateHireAsset { Owner = this };
            dlg.ShowDialog();
        }

        private void bttn_customerCreationDialog_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CreateCustomerWindow { Owner = this };
            dlg.ShowDialog();
        }

        private void dataGrid_Home_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}