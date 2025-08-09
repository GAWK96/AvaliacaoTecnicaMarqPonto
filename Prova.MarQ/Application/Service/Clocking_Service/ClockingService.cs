using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Infra.Loader.Clocking_Loader;


namespace Prova.MarQ.Infra.Service.Clocking_Service
{
    public class ClockingService : IClockingService
    {
        private readonly IClockingLoader _clockingLoader;
        public ClockingService(IClockingLoader clockingLoader)
        {
            _clockingLoader = clockingLoader;
        }
        public async Task<Clocking> ClockIn(Clocking clocking)
        {
            if (clocking.ClockOut != null)
            {
                throw new InvalidOperationException("Clockout must be empty!");
            }
            var filterClockInByDate = await _clockingLoader.FilterClockingByDate(clocking);
           if (filterClockInByDate != null)
            {
                throw new InvalidOperationException("clocking of employee already has been registered!");
            }
            await _clockingLoader.ClockIn(clocking);
            return clocking;
        }

        public async Task ClockOut(string pin, DateTime clockout)
        {
            await _clockingLoader.ClockOut(pin, clockout);
        }

        public async Task<List<ClockingReport>> ClockingReport(DateTime startDate, DateTime endDate, string? document = null)
        {
            var clockingReport = await _clockingLoader.ClockingReport(startDate, endDate, document);
            return clockingReport;
        }

        public string GenerateCsvReport(List<ClockingReport> reportData)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Date,Employee Name,Document,Clockings on the date,Total Worked,overtime,Day of the week");

            foreach (var item in reportData)
            {
                csv.AppendLine($"{item.Date},{item.EmployeeName},{item.EmployeeDocument},{item.ClockingsPerDay},{item.TotalWorked},{item.Overtime},{item.WeekDay}");
            }

            return csv.ToString();
        }
    }
}
