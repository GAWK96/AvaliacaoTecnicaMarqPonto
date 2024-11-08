using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Infra.Loader.Company_Loader;
using Prova.MarQ.Infra.Loader.Employee_Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Prova.MarQ.Infra.Service.Employee_Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeLoader _employeeLoader;

        public EmployeeService(IEmployeeLoader employeeLoader)
        {
            _employeeLoader = employeeLoader;
        }
        public async Task AddEmployee(Employee employee)
        {
            var employeeExists = await _employeeLoader.FilterEmployeeByPin(employee.EmployeePin);
            if (employeeExists != null)
            {
                throw new InvalidOperationException("Employee already registered!");
            }
            if (employee.EmployeeName.Length > 100)
            {
                throw new InvalidOperationException("Employee name must be less than 100 characters!");
            }
            if (employee.EmployeePin.Length > 4)
            {
                throw new InvalidOperationException("Employee pin must be less or equal than 4 characters!");
            }
            await _employeeLoader.AddEmployee(employee);
            await _employeeLoader.SaveChangesBusiness();
        }

        public async Task DeleteEmployee(string employeePin, bool softDelete)
        {
            var filteredEmployee = await FilterEmployeeByPin(employeePin);
            if (filteredEmployee == null)
            {
                throw new InvalidOperationException("Employee not found");
            }
            if (filteredEmployee.IsEmployeeDeleted == true)
            {
                throw new InvalidOperationException("Employee is already deleted");
            }
            await _employeeLoader.SoftDeleteEmployee(filteredEmployee, softDelete);
        }

        public Task<Employee?> FilterEmployeeByPin(string employeePin)
        {
            var filteredEmployee = _employeeLoader.FilterEmployeeByPin(employeePin);
            if (filteredEmployee == null)
            {
                throw new InvalidOperationException("Employee not found");
            }
            return filteredEmployee;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var filteredEmployee = await _employeeLoader.FilterEmployeeByPin(employee.EmployeePin);
            if (filteredEmployee == null)
            {
                throw new InvalidOperationException("Employee not found");
            }
            await _employeeLoader.UpdateEmployee(employee, filteredEmployee);
        }
    }
}
