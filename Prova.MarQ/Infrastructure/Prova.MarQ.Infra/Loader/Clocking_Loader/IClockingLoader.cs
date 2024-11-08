using Prova.MarQ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Infra.Loader.Clocking_Loader
{
    public interface IClockingLoader
    {
        Task ClockIn(Clocking clocking);
        Task ClockOut(string pin, DateTime clockout);
        Task<Clocking?> FilterClockingByDate(Clocking clocking);
        Task<List<ClockingReport>> ClockingReport(DateTime startDate, DateTime endDate, string? document);
    }
}
