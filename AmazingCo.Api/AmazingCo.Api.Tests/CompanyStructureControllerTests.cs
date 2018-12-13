using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AmazingCo.Api.Controllers;
using AmazingCo.Api.Data;
using AmazingCo.Api.Data.Cache;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Wonga.Common.Testing.NUnit;

namespace AmazingCo.Api.Tests
{
    [TestFixture]
    public class CompanyStructureControllerTests : ParallelizableTestFixtureFor<CompanyStructureController>
    {
        public Mock<ICompanyRepository> CompanyRepositoryMock => GetMock<ICompanyRepository>();
        public Mock<ICompanyStructureCache> CompanyStructureCacheMock => GetMock<ICompanyStructureCache>();
        protected override CompanyStructureController ConstructSystemUnderTest()
        {
            return new CompanyStructureController(CompanyRepositoryMock.Object, CompanyStructureCacheMock.Object);
        }

        private readonly Guid? _rootParentId = null;
        private readonly Guid _rootExternalId = Guid.NewGuid();
        private readonly Guid _anodeExternalId = Guid.NewGuid();
        private readonly string _rootName = "root";
        private readonly string _anodeName = "A";
        private readonly string _bnodeName = "B";
        private readonly string _cnodeName = "C";
        private readonly string _dnodeName = "D";
        private readonly string _enodeName = "E";
        private readonly int _rootId = 1;
        private readonly int _anodeId = 2;
        private readonly int _bnodeId = 3;
        private readonly int _cnodeId = 4;
        private readonly int _dnodeId = 5;
        private readonly int _enodeId = 6;
        private readonly string _notExistingCompanyName = "myCompany";


        protected override void SetUp()
        {
            SetUpCompanyRepositoryMock();
            SetUpCompanyStructureCache();
        }

        [Test]
        public void Get_ShouldReturnListOfCompaniesNamesFromCache_WhenRepositoryNotEmpty()
        {
           var result = Sut.Get();

            Assert.IsTrue(result.Value.Contains(_rootName));
            Assert.IsTrue(result.Value.Contains(_anodeName));
            Assert.IsTrue(result.Value.Contains(_bnodeName));
            Assert.IsTrue(result.Value.Contains(_cnodeName));
            Assert.IsTrue(result.Value.Contains(_dnodeName));
        }

        [Test]
        public void GetWithStrParametr_ShouldReturnCompanyChildrensFromCache_WhenCompanyNamePresent()
        {
            SetUpChildrensStructureInForRootCacheRepo();

            var result = Sut.Get(_rootName);

            Assert.IsTrue(result.Value.Count() == 4);
            Assert.IsNotNull(result.Value.Where(i => i.Name == _anodeName));
            Assert.IsNotNull(result.Value.Where(i => i.Name == _bnodeName));
            Assert.IsNotNull(result.Value.Where(i => i.Name == _cnodeName));
            Assert.IsNotNull(result.Value.Where(i => i.Name == _dnodeName));
        }

        [Test]
        public void GetWithStrParametr_ShouldReturnNotFoundResult_WhenCompanyNameNotPresent()
        {
            var result = Sut.Get(_notExistingCompanyName);

            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public void Put_ShouldUpdateParentNodeOfGivenNode_WhenCompanyPresent()
        {
            var oldParentId = Guid.NewGuid();
            SetUpCompanyRepositoryMock(_dnodeId, _dnodeName, Guid.NewGuid(), oldParentId);

            IActionResult result = Sut.Put(_dnodeName, new CompanyApiEntity {Name = _dnodeName, ParentName = _anodeName});

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
            CompanyRepositoryMock.Verify(i => i.UpdateNode(It.Is<Company>(c => c.Name == _dnodeName)));
            CompanyRepositoryMock.Verify(i => i.UpdateNode(It.Is<Company>(c => c.ParentId == _anodeExternalId)));
        }

        private void SetUpCompanyRepositoryMock(int nodeId, string name, Guid externalId, Guid paretnerId)
        {
            var node = new Company
            {
                Id = nodeId,
                Name = name,
                ExternalId = externalId,
                ParentId = paretnerId
            };

            CompanyRepositoryMock.Setup(repository => repository.GetNode(nodeId)).Returns(node);
        }

        private void SetUpChildrensStructureInForRootCacheRepo()
        {
            var companies = GetCompaniesTreeStructure(false);

            CompanyStructureCacheMock.Setup(repository => repository.GetChildrens(_rootName)).Returns(companies);
        }

        private void SetUpCompanyStructureCache()
        {
            var companies = GetCompaniesTreeStructure();

            CompanyStructureCacheMock.Setup(repository => repository.Companies).Returns(companies);
        }

        private void SetUpCompanyRepositoryMock()
        {
            var companies = GetCompaniesTreeStructure();

            CompanyRepositoryMock.Setup(repository => repository.GetAllNodes()).Returns(companies);
        }

        private List<Company> GetCompaniesTreeStructure(bool withRoot = true)
        {
            var bExternalId = Guid.NewGuid();
            var companies = new List<Company>
            {
                new Company { Height = 1, Name = _anodeName, Id = _anodeId, ParentId = _rootExternalId, ExternalId = _anodeExternalId},
                new Company { Height = 1, Name = _bnodeName, Id = _dnodeId, ParentId = _rootExternalId, ExternalId = bExternalId},
                new Company { Height = 2, Name = _cnodeName, Id = _cnodeId, ParentId = _anodeExternalId, ExternalId = Guid.NewGuid()},
                new Company { Height = 2, Name = _dnodeName, Id = _dnodeId, ParentId = bExternalId, ExternalId = Guid.NewGuid()}
            };
            if (withRoot)
            {
                companies.Add(new Company { Height = 0, Name = _rootName, Id = _rootId, ParentId = _rootParentId, ExternalId = _rootExternalId });
            }

            return companies;
        }
    }
}
