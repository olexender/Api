using System.Collections.Generic;

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

        public void UpdateNode(Company company)
        {
            Update(company);
        }
    }
}
