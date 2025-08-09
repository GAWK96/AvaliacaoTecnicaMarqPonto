using Prova.MarQ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Infra.Service.Employee_Service
{
    public interface IEmployeeService
    {
        Task<Employee?> FilterEmployeeByPin(string employeePin);
        Task AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(string pin, bool softDelete);
    }
}
