using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prova.MarQ.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Prova.MarQ.Infra;

namespace Prova.MarQ.Infra.Loader.Company_Loader
{
    public class CompanyLoader : BaseLoader, ICompanyLoader
    {
        private readonly ProvaMarqDbContext _context;
        //private Task<Company>? company;
        public CompanyLoader(ProvaMarqDbContext context): base(context)
        {
            _context = context;
        }
        public async Task AddCompany(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public async Task<Company?> FilterCompanyById(int id)
        {
            var company = await _context.Companies
                            .Where(x => x.CompanyId == id)
                            .FirstOrDefaultAsync();
            return company;
        }

            public async Task<bool> SoftDeleteCompany(bool softDelete, Company company)
        {
            company.IsCompanyDeleted = softDelete;
            await SaveChangesBusiness();
            return true; 
        }

        public async Task UpdateCompany(Company company, Company filteredCompany)
        {
            var updateCompany = await FilterCompanyById(company.CompanyId);
            filteredCompany.CompanyId = company.CompanyId;
            filteredCompany.CompanyName = company.CompanyName;
            filteredCompany.CompanyDocument = company.CompanyDocument;
            await SaveChangesBusiness();
        }
    }
}
