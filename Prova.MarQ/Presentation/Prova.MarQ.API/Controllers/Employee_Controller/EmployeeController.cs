using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Infra.Service.Company_Service;
using Prova.MarQ.Infra.Service.Employee_Service;

namespace Prova.MarQ.API.Controllers.Employee_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase, IEmployeeController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            await _employeeService.AddEmployee(employee);
            return Ok("Employee added successfully");
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(string pin, bool softDelete)
        {
            await _employeeService.DeleteEmployee(pin, softDelete);
            return Ok("Employee deleted successfully");
        }

        [HttpGet("GetEmployeeByPin")]
        public async Task<IActionResult> GetEmployeeByPin(string pin)
        {
            var employeeFiltered = await _employeeService.FilterEmployeeByPin(pin);
            return Ok(employeeFiltered);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            await _employeeService.UpdateEmployee(employee);
            return Ok("Employee updated successfully");
        }
    }
}
