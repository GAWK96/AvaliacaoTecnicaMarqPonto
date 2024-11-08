using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Infra.Loader.Clocking_Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Infra.Service.Clocking_Service
{
    public interface IClockingService
    {
        Task<Clocking> ClockIn(Clocking clocking);
        Task ClockOut(string pin, DateTime clockout);
        Task<List<ClockingReport>> ClockingReport(DateTime startDate, DateTime endDate, string? document = null);
        public string GenerateCsvReport(List<ClockingReport> reportData);
    }
}
