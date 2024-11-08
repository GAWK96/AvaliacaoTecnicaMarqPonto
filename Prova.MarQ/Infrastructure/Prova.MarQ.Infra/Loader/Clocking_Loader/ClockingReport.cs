using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Infra.Loader.Clocking_Loader
{
    public class ClockingReport
    {
        public DateTime Date { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeDocument { get; set; }
        public int ClockingsPerDay { get; set; }
        public string? EmployeePin { get; set; }
        public TimeSpan TotalWorked { get; set; }
        public TimeSpan Overtime { get; set; }
        public DayOfWeek WeekDay { get; set; }
    }
}
