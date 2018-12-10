using System;
using System.Collections.Generic;
using System.Linq;
using AmazingCo.Api.Data;
using AmazingCo.Api.Data.Cache;
using Microsoft.AspNetCore.Mvc;

namespace AmazingCo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyStructureController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        private readonly ICompanyStructureCache _companyStructureCache;

        public CompanyStructureController(ICompanyRepository companyRepository, ICompanyStructureCache companyStructureCache)
        {
            _companyRepository = companyRepository;
            _companyStructureCache = companyStructureCache;
        }
        // GET api/companystructure
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var res = _companyStructureCache.Companies;
 
            return res.Select(i => i.Name).ToArray();
        }

        // GET api/companystructure/5
        [HttpGet("{node}")]
        public ActionResult<IEnumerable<Company>> Get(string node)
        {
            if (_companyStructureCache.Companies.FirstOrDefault(i => i.Name == node) == null)
            {
                return NotFound("Not found");
            }

            return _companyStructureCache.GetChildrens(node).ToList();
        }

        // PUT api/companystructure/5
        [HttpPut("{node}")]
        public IActionResult Put(string node, [FromBody] CompanyApiEntity company)
        {
           if (company == null)
           {
               return BadRequest();
           }
           
          var currentNode = _companyStructureCache.Companies.FirstOrDefault(i => i.Name == node);
          var parentNode = _companyStructureCache.Companies.FirstOrDefault(i => i.Name == company.ParentName);
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