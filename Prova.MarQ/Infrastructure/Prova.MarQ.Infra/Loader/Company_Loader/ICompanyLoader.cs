using Prova.MarQ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Infra.Loader.Company_Loader
{
    public interface ICompanyLoader
    {
        Task<Company?> FilterCompanyById(int id);
        Task AddCompany(Company company);
        Task UpdateCompany(Company company, Company filteredCompany);

        Task<bool> SoftDeleteCompany(bool softDelete, Company company);
        Task SaveChangesBusiness();
    }
}
