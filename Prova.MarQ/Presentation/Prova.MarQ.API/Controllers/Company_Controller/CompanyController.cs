using Microsoft.AspNetCore.Mvc;
using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Infra.Service.Company_Service;

namespace Prova.MarQ.API.Controllers.Company_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase, ICompanyController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany([FromBody] Company company)
        {
            await _companyService.AddCompany(company);
            return Ok("Company added successfully");
        }

        [HttpDelete("DeleteCompany")]
        public async Task<IActionResult> DeleteCompany(bool softDelete, int id)
        {
            await _companyService.DeleteCompany(softDelete, id);
            return Ok("Product deleted successfully");
        }
        [HttpGet("GetCompanyById")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var companyFiltered = await _companyService.FilterCompanyById(id);
            return Ok(companyFiltered);
        }
        [HttpPut("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany([FromBody] Company company)
        {
            await _companyService.UpdateCompany(company);
            return Ok("Company updated successfully");
        }
    }
}
