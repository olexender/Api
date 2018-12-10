using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AmazingCo.Api.Data
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(CompanyStructureContext dbContext)
            : base(dbContext)
        {

        }

        public Company GetNode(int nodeId)
        {
            return GetById(nodeId);
        }

        public async void UpdateStructure(IEnumerable<Company> companies)
        {
            await UpdateRange(companies);
        }

        public  IEnumerable<Company> GetAllNodes()
        {
            return  GetAll();
        }

        public async void UpdateNode(Company company)
        {
            await Update(company);
        }
    }
}
