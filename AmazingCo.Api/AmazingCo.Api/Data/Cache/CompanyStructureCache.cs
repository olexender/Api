using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AmazingCo.Api.Data.Cache
{
    public class CompanyStructureCache : ICompanyStructureCache
    {
        private readonly ICompanyRepository _companiesRepository;
        private static object _lockAllCompanies = new object();
        private IEnumerable<Company> _allCompanies;
        private static object _lockChildrens = new object();
        private readonly Dictionary<string, List<Company>> _childrens = new Dictionary<string, List<Company>>();

        public CompanyStructureCache(ICompanyRepository companiesRepository)
        {
            _companiesRepository = companiesRepository;
        }

        public IEnumerable<Company> Companies
        {
            get
            {
                lock (_lockAllCompanies)
                {
                    if (_allCompanies == null)
                    {
                        _allCompanies = _companiesRepository.GetAllNodes().ToList();
                    }
                }

                return _allCompanies;
            }
        }

        public IEnumerable<Company> GetChildrens(string node)
        {
            List<Company> result;
            lock (_lockChildrens)
            {
                if (!_childrens.TryGetValue(node, out result))
                {
                    var currentNode = _allCompanies.FirstOrDefault(i => i.Name == node);
                    result = GetNodeChildren(currentNode, Companies).ToList();
                    _childrens[node] = result;
                }
            }

            return result;
        }

        IEnumerable<Company> GetNodeChildren(Company node, IEnumerable<Company> nodes)
        {
            var children = nodes.Where(n => n.ParentId == node.ExternalId);

            if (children.Any())
            {
                foreach (Company child in children)
                {
                    yield return child;

                    var grandchildren = GetNodeChildren(child, nodes);
                    foreach (Company grandchild in grandchildren)
                    {
                        yield return grandchild;
                    }
                }
            }
        }

        public void Reset()
        {
            lock (_lockAllCompanies)
            {
                _allCompanies = null;
            }
        }
    }
}
