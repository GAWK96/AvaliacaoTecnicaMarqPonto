using Microsoft.AspNetCore.Mvc;
using Prova.MarQ.Domain.Entities;

namespace Prova.MarQ.API.Controllers.Employee_Controller
{
    public interface IEmployeeController
    {
        Task<IActionResult> AddEmployee(Employee company);
        Task<IActionResult> GetEmployeeByPin(string pin);
        Task<IActionResult> UpdateEmployee([FromBody] Employee company);
        Task<IActionResult> DeleteEmployee(string pin, bool softDelete);
    }
}
