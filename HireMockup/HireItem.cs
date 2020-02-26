using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireMockup
{
    class HireItem
    {
        // Properties of the HireItem
        public string HireID { get; set; }
        public string HireName { get; set; }
        public double DailyRate { get; set; }

        private double weeklyRate;

        public double WeeklyRate
        {
            get { return DailyRate * 3; }
            
        }

        public override string ToString()
        {
            return HireID + " - " + HireName + " - Daily Rate - " + DailyRate + " - Weekly Rate - " + WeeklyRate; 
        }



    }
}
