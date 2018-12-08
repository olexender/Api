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

        public Task<IEnumerable<Company>> GetChildrenNodes()
        {
            throw new NotImplementedException();
        }

        public Company GetChildrenNodes(string node)
        {
            throw new NotImplementedException();
        }

        public  IEnumerable<Company> GetAllNodes()
        {
            return  GetAll();
        }
    }
}
