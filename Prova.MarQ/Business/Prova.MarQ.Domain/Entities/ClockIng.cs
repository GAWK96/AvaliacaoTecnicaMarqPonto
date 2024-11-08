using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Domain.Entities
{
    public class Clocking
    {
        public int Id { get; set; }
        public string EmployeePin { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public Employee Employee { get; set; }
    }
}
