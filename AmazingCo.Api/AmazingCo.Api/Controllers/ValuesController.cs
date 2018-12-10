using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AmazingCo.Api.Data;
using AmazingCo.Api.Data.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace AmazingCo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        private readonly ICompanyStructureCache _companyStructureCache;
        // GET api/values

        public ValuesController(ICompanyRepository companyRepository, ICompanyStructureCache companyStructureCache)
        {
            _companyRepository = companyRepository;
            _companyStructureCache = companyStructureCache;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var res = _companyStructureCache.Companies;
            //var res1 = _companyRepository.GetChildrenNodes("A");
            return res.Select(i => i.Name).ToArray();
        }

        // GET api/values/5
        [HttpGet("{node}")]
        public ActionResult<IEnumerable<Company>> Get(string node)
        {
            if (_companyStructureCache.Companies.FirstOrDefault(i => i.Name == node) == null)
            {
                return NotFound("Not found");
            }

            //_companyRepository.
            return _companyStructureCache.GetChildrens(node).ToList();
        }

        // PUT api/values/5
        [HttpPut("{node}")]
        public IActionResult Put(string node, [FromBody] Company company)
        {
           if (company == null)
           {
               return BadRequest();
           }
           
          var currentNode = _companyStructureCache.Companies.FirstOrDefault(i => i.Name == node);
          var parentNode = _companyStructureCache.Companies.FirstOrDefault(i => i.ExternalId == company.ParentId);
          if (currentNode == null || parentNode == null || node != company.Name)
          {
              return BadRequest();
          }
          
          UpdateNode(currentNode.Id, parentNode.ExternalId);
          
          return NoContent();
        }

        private void UpdateNode(int nodeId, Guid newParentId)
        {
            var node = _companyRepository.GetNode(nodeId);
            node.ParentId = newParentId;
            _companyRepository.UpdateNode(node);
            _companyStructureCache.Reset();
        }
    }
}