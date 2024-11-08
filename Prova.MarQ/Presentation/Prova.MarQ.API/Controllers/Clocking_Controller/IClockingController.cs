using Microsoft.AspNetCore.Mvc;
using Prova.MarQ.Domain.Entities;

namespace Prova.MarQ.API.Controllers.Clocking_Controller
{
    public interface IClockingController
    {
        Task<IActionResult> ClockIn(Clocking clocking);
        Task<IActionResult> ClockOut(string pin, DateTime clockout);

        Task<OkObjectResult> ClockingReport(DateTime startDate, DateTime endDate, string? document = null);
        Task<IActionResult> ExportCsv(DateTime startDate, DateTime endDate, string? document);
    }
}
