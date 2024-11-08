using Microsoft.AspNetCore.Mvc;
using Prova.MarQ.Domain.Entities;

namespace Prova.MarQ.API.Controllers.Company_Controller
{
    public interface ICompanyController
    {
        Task<IActionResult> AddCompany(Company company);
        Task<IActionResult> GetCompanyById(int id);
        Task<IActionResult> UpdateCompany([FromBody] Company company);
        Task<IActionResult> DeleteCompany(bool softDelete, int id);
    }
}
