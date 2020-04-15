using System;
using System.Data.SqlClient;
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
        //Create a connection to the DB
        private Model1Container db = new Model1Container();

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
            string hireName = tbx_addHireName.Text.ToString();
            string hireType = tbx_addHireType.Text.ToString();
            decimal hireDailyRate = Decimal.Parse(tbx_addHireDailyRate.Text);
            newHireAsset(hireName, hireType, hireDailyRate);
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

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from em in db.Employees
                        select em;

            var result = query.ToList();

            employeeDataGrid.ItemsSource = result;

            totalSalaryCalculation();
        }

        // Method to set/update total salary calculation that we can can when the form loads and when a new employee is created
        public void totalSalaryCalculation()
        {
            var query2 = from em in db.Employees
                         select em.weeklySalary;

            decimal totalWeeklySalaryBill = query2.Sum();

            lbl_wageTotal.Content = totalWeeklySalaryBill.ToString("C2");
        }

        [Obsolete]
        public void newHireAsset(string hireName, string hireType, decimal dailyRate)
        {
            try
            {
                SqlConnection sql = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\DATABASES\MULTIHIREDB.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                sql.Open();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO HireAssets (hireName, hireType, dailyRate) VALUES (@Name, @Type, @dailyRate)", sql);
                sqlCommand.Parameters.Add("@Name", hireName);
                sqlCommand.Parameters.Add("@Type", hireType);
                sqlCommand.Parameters.Add("@dailyRate", dailyRate);
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Completed correctly");
            }
        }

        private void employeeDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Console.WriteLine(employeeDataGrid.SelectedItem.ToString());
        }

        private void button_Click_2(object sender, RoutedEventArgs e)
        {
            using(var context = new Model1Container())
            {
                try
                {
                    int selectedEmployee = int.Parse(tbx_selectedEmployee.Text);
                    var employee = context.Employees.Find(selectedEmployee);
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                }
                catch(System.Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    var query = from em in db.Employees
                                select em;

                    var result = query.ToList();

                    employeeDataGrid.ItemsSource = result;

                    totalSalaryCalculation();
                }
                
            }
        }
        
    }
}