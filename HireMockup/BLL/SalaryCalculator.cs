using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireMockup.BLL
{
    public class SalaryCalculator
    {
        public enum JobTitles
        {
            Manager,
            Admin,
            Mechanic,
            HireOperator
        }

        public static decimal CalculateSalary(string position, decimal weeksWorked)
        {
            decimal totalSalary = 0;

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

            return totalSalary;
               
        }
       
    }
}
