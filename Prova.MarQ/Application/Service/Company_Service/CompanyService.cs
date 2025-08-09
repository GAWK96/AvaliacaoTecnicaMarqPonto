using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Infra.Loader.Company_Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prova.MarQ.Infra.Loader;

namespace Prova.MarQ.Infra.Service.Company_Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyLoader _companyLoader;
        //private readonly Prova.MarQ.Infra.Loader.Company_Loader.CompanyLoader companyLoader;
        public CompanyService(ICompanyLoader companyLoader)
        {
            _companyLoader = companyLoader;
        }
        public async Task AddCompany(Company company)
        {
            var filteredCompany = await _companyLoader.FilterCompanyById(company.CompanyId);
            if (filteredCompany != null)
            {
                throw new InvalidOperationException("Company already registered!");
            }
            if (company.CompanyName.Length > 100)
            {
                throw new InvalidOperationException("Company name must be less than 100 characters!");
            }
            await _companyLoader.AddCompany(company);
            await _companyLoader.SaveChangesBusiness();
        }

        public async Task DeleteCompany(bool softDelete, int id)
        {
            var filteredCompany = await _companyLoader.FilterCompanyById(id);
            if (filteredCompany == null)
            {
                throw new InvalidOperationException("Company not found!");
            }
            await _companyLoader.SoftDeleteCompany(softDelete, filteredCompany);
        }

        public async Task<Company?> FilterCompanyById(int id)
        {
            var filteredCompany = await _companyLoader.FilterCompanyById(id);
            if (filteredCompany == null)
            {
                throw new InvalidOperationException("Company not found!");
            }
            return filteredCompany;
        }

        public async Task UpdateCompany(Company company)
        {
            var filteredCompany =  await _companyLoader.FilterCompanyById(company.CompanyId);
            if (filteredCompany == null)
            {
                throw new InvalidOperationException("Company not found!");
            }
            await _companyLoader.UpdateCompany(company, filteredCompany);
        }

    }
}
