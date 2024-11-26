using Microsoft.EntityFrameworkCore;
using Prova.MarQ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Prova.MarQ.Infra.Loader.Employee_Loader
{
    public class EmployeeLoader : BaseLoader, IEmployeeLoader
    {
        private readonly ProvaMarqDbContext _context; 
        public EmployeeLoader(ProvaMarqDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await SaveChangesBusiness();
        }
        public async Task<Employee?> FilterEmployeeByPin(string pin)
        {
            var employeeByPin = await _context.Employees
                            .Where(x => x.EmployeePin == pin)
                            .FirstOrDefaultAsync();
            return employeeByPin;
        }

        public async Task<bool> SoftDeleteEmployee(Employee employee, bool softDelete)
        {
            employee.IsEmployeeDeleted = softDelete;
            await SaveChangesBusiness();
            return true;
        }

        public async Task UpdateEmployee(Employee employee, Employee filteredEmployee)
        {
            filteredEmployee.EmployeePin = employee.EmployeePin;
            filteredEmployee.EmployeeName = employee.EmployeeName;
            filteredEmployee.EmployeeDocument = employee.EmployeeDocument;
            filteredEmployee.CompanyId = employee.CompanyId;
            await SaveChangesBusiness();
        }
    }
}
