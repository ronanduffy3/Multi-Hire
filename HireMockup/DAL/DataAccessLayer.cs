﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HireMockup.BLL;

namespace HireMockup.DAL
{
    public class DataAccessLayer
    {
        #region Connection Strings
        
        // Create an instance of the database here
        private Model1Container db = new Model1Container();
        
        #endregion

        #region Data Retrieval

        #region Hire Item Queries

        // List all HireAssets
        public static DataTable ListHireAssets()
        {
            DataTable tb1 = new DataTable();
            

            tb1.Columns.Add("Asset ID", typeof(int));
            tb1.Columns.Add("Asset Name", typeof(string));
            tb1.Columns.Add("Asset Type", typeof(string));
            tb1.Columns.Add("Daily Rate", typeof(decimal));
            // Add an extra column that is calculated locally rather than stored in the DB 
            tb1.Columns.Add("Weekly Rate", typeof(decimal));

            using(var context = new Model1Container())
            {
                var query = (from ha in context.HireAssets
                             select ha).ToList();

                foreach(var hireasset in query)
                {
                    // hiresset.dailyRate*3 represents three days hire covering a weeks hire rate
                    tb1.Rows.Add(hireasset.hireID, hireasset.hireName, hireasset.hireType, hireasset.dailyRate, (hireasset.dailyRate*3));
                }

            }
            return tb1;
        }

        // Method to search assets by type
        public static DataTable HireAssetSearch(string searchValue)
        {
            DataTable tb1 = new DataTable();

            tb1.Columns.Add("Asset ID", typeof(int));
            tb1.Columns.Add("Asset Name", typeof(string));
            tb1.Columns.Add("Asset Type", typeof(string));
            tb1.Columns.Add("Daily Rate", typeof(decimal));
            tb1.Columns.Add("Weekly Rate", typeof(decimal));

            using (var context = new Model1Container()) 
            {
                var query = (from ha in context.HireAssets
                             where ha.hireType == searchValue
                             select ha).ToList();

                foreach(var result in query)
                {
                    tb1.Rows.Add(result.hireID, result.hireName, result.hireType, result.dailyRate, (result.dailyRate * 3));
                }
            }
            return tb1;

        }

        // Method to write a new hire asset to the table, used older SQL libray here but used entity elsewhere. Works ok anyways
        [Obsolete]
        public static void newHireAsset(string hireName, string hireType, decimal dailyRate)
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
            catch(SqlException ex)
            {
                MessageBox.Show($"Following error has occured: {ex}");
            }
            finally
            {
                MessageBox.Show("Item added successfully.");
            }
        }

        public static List<HireAsset> GetHireList()
        {
            List<HireAsset> hireList = new List<HireAsset>();
            using (var context = new Model1Container())
            {
                var query = (from ha in context.HireAssets
                             select ha).ToList();

                foreach (var c in query)
                {
                    hireList.Add(c);
                }
            }
            return hireList;
        }

        #endregion

        #region Employee Queries

        // All employee queries from here down
        public static DataTable ListEmployee()
        {
            // Method that returns a datatable of all employees
            DataTable tb1 = new DataTable();

            // Define the correct table headers/columns
            tb1.Columns.Add("Employee ID", typeof(int));
            tb1.Columns.Add("Employee Forename", typeof(string));
            tb1.Columns.Add("Employee Surname", typeof(string));
            tb1.Columns.Add("Weekly Salary", typeof(decimal));
            tb1.Columns.Add("Contact Number", typeof(string));
            tb1.Columns.Add("Job Title", typeof(string));

            using (var context = new Model1Container())
            {
                // Query to return all employees
                var query = (from e in context.Employees
                             select e).ToList();

                foreach (var employee in query)
                {
                    tb1.Rows.Add(employee.Id, employee.employeeName, employee.employeeSurname, employee.weeklySalary, employee.contactNumber, employee.jobTitle);
                }
            }
            return tb1;
        }

        // Total Salary Calculation
        public static decimal CalculateSalaryBill()
        {
            decimal totalSalaryBill = 0;

            using(var context = new Model1Container())
            {
                var query = from e in context.Employees
                            select e.weeklySalary;

                totalSalaryBill = query.Sum();

            }

            return totalSalaryBill;
            
        }

        // Use an enum to calculate the certain salary
        public static decimal CalculateCertainSalary(string jobTitle)
        {
            decimal salaryBill = 0;

            using (var context = new Model1Container())
            { 
                var query = from e in context.Employees
                            where e.jobTitle == jobTitle
                            select e.weeklySalary;
                if (query != null)
                {
                    salaryBill = query.Sum();
                }
                else
                {
                    salaryBill = 0;
                }

            }
            return salaryBill;

        }

        // Remove an employee from DB
        public static void RemoveEmployeeByID(int selectedEmployee)
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to remove employee {selectedEmployee.ToString()}", "Are you sure?", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    using (var context = new Model1Container())
                    {
                        try
                        {
                            var employee = context.Employees.Find(selectedEmployee);
                            context.Employees.Remove(employee);
                            context.SaveChanges();
                        }
                        catch (SystemException ex)
                        {
                            MessageBox.Show($"Exception: {ex}");
                        }
                    }
                    break; 
                case MessageBoxResult.No:
                    MessageBox.Show("Employee Not Remove");
                    break;
            }

            
        }

        #endregion

        #endregion


    }
}
