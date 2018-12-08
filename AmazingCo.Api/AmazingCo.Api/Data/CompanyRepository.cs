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

        public async Task<Company> GetChildrenNodes()
        {
            return await GetAll()
                .OrderByDescending(c => c.Name)
                .FirstOrDefaultAsync();
        }

        public Task<string> GetChildrenNodes(string node)
        {
            throw new NotImplementedException();
        }
    }
}
