using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireMockup.BLL
{
    public class SalaryCalculator
    {
        // Define the job titles available
        public enum JobTitles
        {
            Manager,
            Admin,
            Mechanic,
            HireOperator
        }

        public static decimal CalculateSalary(string position, decimal weeksWorked)
        {
            // Initiliaze a decimal to hold the salary to be returned
            decimal totalSalary = 0;

            // 4 positions with a specific salary simply multiply weeks worked by salary
            switch (position)
            {
                case "Manager":
                    totalSalary = 625 * weeksWorked;
                    break;
                case "HireOperator":
                    totalSalary = 550 * weeksWorked;
                    break;
                case "Mechanic":
                    totalSalary = 495 * weeksWorked;
                    break;
                case "Admin":
                    totalSalary = 350 * weeksWorked;
                    break;
            }

            // Return the decimal
            return totalSalary;
               
        }
       
    }
}
