using Microsoft.EntityFrameworkCore;
using Prova.MarQ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Prova.MarQ.Infra.Loader.Clocking_Loader
{
    public class ClockingLoader : BaseLoader, IClockingLoader
    {
        private readonly ProvaMarqDbContext _context;
        //private Task<Company>? company;
        public ClockingLoader(ProvaMarqDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Clocking?> FilterClockingByDate(Clocking clocking)
        {
            var getClockInDate = clocking.ClockIn.Date;
            var clockInFilteredByDate = await _context.Clocking
                                                .Where(x => x.ClockIn.Date == getClockInDate && x.EmployeePin == clocking.EmployeePin)
                                                .FirstOrDefaultAsync();
            return clockInFilteredByDate;
        }
        public async Task ClockIn(Clocking clocking)
        {
            await _context.AddAsync(clocking);
            await SaveChangesBusiness();
        }

        public async Task ClockOut(string pin, DateTime clockout)
        {
            var setClockout = await FilterClockingByPin(pin, clockout);
            if (setClockout == null)
            {
                throw new InvalidOperationException("clocking of employee has not been registered!");
            }
            setClockout.ClockOut = clockout;
            await SaveChangesBusiness();
        }

        private async Task<Clocking?> FilterClockingByPin(string pin, DateTime clockout)
        {
            var clockingFiltered = await _context.Clocking
                            .Where(x => x.EmployeePin == pin && x.ClockIn.Date == clockout.Date)
                            .FirstOrDefaultAsync();
            return clockingFiltered;
        }


        private IQueryable<Clocking> ClockingFilteredReport(DateTime startDate, DateTime endDate, string? document)
        {

            if (!string.IsNullOrEmpty(document))
            {
               var filteredData =  _context.Clocking
                                       .Include(x => x.Employee)
                                       .Where(x => x.Employee.EmployeeDocument == document &&
                                                   x.ClockIn.Date >= startDate.Date && x.ClockIn.Date <= endDate.Date);
                return filteredData;
            }
            else
            {
                var filteredData =  _context.Clocking
                                       .Include(x => x.Employee)
                                       .Where(x => x.ClockIn.Date >= startDate.Date && x.ClockIn.Date <= endDate.Date);
                return filteredData;
            }
        }

        public async Task<List<ClockingReport>> ClockingReport(DateTime startDate, DateTime endDate, string? document)
        {
            IQueryable<Clocking> filteredData;
            filteredData = ClockingFilteredReport(startDate, endDate, document);

            var result = await (from t1 in filteredData
                          join t2 in _context.Employees
                          on t1.EmployeePin equals t2.EmployeePin
                          select new
                          {
                              Date = t1.ClockIn.Date,
                              EmployeeName = t2.EmployeeName,
                              EmployeeDocument = t2.EmployeeDocument,
                              ClockIn = t1.ClockIn,
                              ClockOut = t1.ClockOut
                          }).ToListAsync();

            var finalReport = result
                               .Select(x => new ClockingReport
                                     {
                                         Date = x.Date,
                                         EmployeeName = x.EmployeeName,
                                         EmployeeDocument = x.EmployeeDocument,
                                         ClockingsPerDay = x.ClockOut != null ? 2 : 1,
                                         TotalWorked = CalculateTotalWorkedTime(x.ClockIn, x.ClockOut),
                                         WeekDay = x.ClockIn.DayOfWeek,
                                         Overtime = CalculateOvertime(CalculateTotalWorkedTime(x.ClockIn, x.ClockOut))
                               }).ToList();
            return finalReport;

        }
        private TimeSpan CalculateTotalWorkedTime(DateTime clockIn, DateTime? clockOut)
        {
            TimeSpan total = TimeSpan.Zero;
            total = (clockOut ?? clockIn) - clockIn;
            return total;
        }

        private TimeSpan CalculateOvertime(TimeSpan totalWorked)
        {
            TimeSpan overtime = totalWorked > TimeSpan.FromHours(8)
                    ? totalWorked - TimeSpan.FromHours(8)
                    : TimeSpan.Zero;
            return overtime;
        }

    }
}
