using Prova.MarQ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Infra.Loader.Employee_Loader
{
    public interface IEmployeeLoader
    {
        Task<Employee?> FilterEmployeeByPin(string pin);
        Task AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee, Employee filteredEmployee);
        Task<bool> SoftDeleteEmployee(Employee employee, bool softDelete);
        Task SaveChangesBusiness();
    }
}
