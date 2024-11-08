using Microsoft.AspNetCore.Mvc;
using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Infra.Loader.Clocking_Loader;
using Prova.MarQ.Infra.Service.Clocking_Service;
using Prova.MarQ.Infra.Service.Company_Service;
using System.Text;

namespace Prova.MarQ.API.Controllers.Clocking_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClockingController : ControllerBase, IClockingController
    {
        private readonly IClockingService _clockingService;

        public ClockingController(IClockingService clockingService)
        {
            _clockingService = clockingService;
        }
        [HttpPost("ClockIn")]
        public async Task<IActionResult> ClockIn([FromBody] Clocking clocking)
        {
            await _clockingService.ClockIn(clocking);
            return Ok("ClockIn successfully!");
        }
        [HttpPut("ClockOut")]
        public async Task<IActionResult> ClockOut(string pin, DateTime clockout)
        {
            await _clockingService.ClockOut(pin, clockout);
            return Ok("ClockOut successfully!");
        }

        [HttpGet("ClockingReport")]
        public async Task<OkObjectResult> ClockingReport(DateTime startDate, DateTime endDate, string? document = null)
        {
            var clockingReport = await _clockingService.ClockingReport(startDate, endDate, document);
            return Ok(clockingReport);
        }

        [HttpGet("exportCsv")]
        public async Task<IActionResult> ExportCsv(DateTime startDate, DateTime endDate, string? document)
        {
            var reportData = await _clockingService.ClockingReport(startDate, endDate, document);
            var csvData = _clockingService.GenerateCsvReport(reportData);
            var fileBytes = Encoding.UTF8.GetBytes(csvData);
            var fileName = $"report_{DateTime.Now:yyyyMMddHHmmss}.csv";
            return File(fileBytes, "text/csv", fileName);
        }

    }
}
