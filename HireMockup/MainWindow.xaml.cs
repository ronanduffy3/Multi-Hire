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

namespace HireMockup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            employeeDataGrid.DataContext = query.DefaultView;

            decimal salaryBill = DataAccessLayer.CalculateSalaryBill();
            lbl_wageTotal.Content = salaryBill.ToString("C");
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
        private void listBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<Contracts> contracts = new List<Contracts>();
            contracts = Contracts.ListRandomContracts();

            lbx_contracts.ItemsSource = contracts.ToList();
        }

        private void lbx_contracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}