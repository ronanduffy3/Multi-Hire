using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HireMockup.DAL;
using HireMockup.BLL;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows.Markup.Localizer;

namespace HireMockup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contracts> contracts = new List<Contracts>();

        DAL.DataAccessLayer dataAccessLayer = new DataAccessLayer();
        //Create a connection to the DB
        private Model1Container db = new Model1Container();

        public MainWindow()
        {
            InitializeComponent();
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
            DataTable query_results = DataAccessLayer.AllCustomers();

            dataGrid_Home.DataContext = null;
            dataGrid_Home.DataContext = query_results.DefaultView;
        }

        // Create a new Hire Asset
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string hireName = tbx_addHireName.Text.ToString();
            string hireType = tbx_addHireType.Text.ToString();
            decimal hireDailyRate = Decimal.Parse(tbx_addHireDailyRate.Text);
            DataAccessLayer.newHireAsset(hireName, hireType, hireDailyRate);
        }

        // Customer Search Function
        private void btn_customerSearch_Click(object sender, RoutedEventArgs e)
        {
            string[] query_strings = customerSearchBox.Text.ToString().Split(' ');
            DataTable query_results = DataAccessLayer.CustomerSearch(query_strings[0], query_strings[1]);
            dataGrid_Home.DataContext = query_results.DefaultView;

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

        // Remove Employee
        private void button_Click_2(object sender, RoutedEventArgs e)
        {
            int selectedEmployee = 0;
            try
            {
                int.TryParse(tbx_selectedEmployee.Text, out selectedEmployee);

            }
            catch(SystemException ex)
            {
                MessageBox.Show($"Invalid Input Exception: {ex}");
            }
            DataAccessLayer.RemoveEmployeeByID(selectedEmployee);

            // Refresh Code
            decimal newSalaryTotal = DataAccessLayer.CalculateSalaryBill();
            lbl_wageTotal.Content = newSalaryTotal.ToString("C");
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

        private void lbx_contracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_saveToFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Contracts.WriteContractsToFile(contracts);
                MessageBox.Show("Contracts saved to c:/temp");
            }
            catch(SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            decimal weeksWorked;
            string position = cbx_salaryCalculator.SelectedItem.ToString();
            Console.WriteLine(position);

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

        private void button2_Click(object sender, RoutedEventArgs e)
        {
           int contractID;
           int.TryParse(tbx_ContractID.Text, out contractID);
           DataAccessLayer.BillCustomer(contracts, contractID);
            lbx_contracts.ItemsSource = null;
            lbx_contracts.ItemsSource = contracts.ToList();

        }
    }
}