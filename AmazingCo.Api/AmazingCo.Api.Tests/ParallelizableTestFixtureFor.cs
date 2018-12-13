using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AmazingCo.Api.Tests;
using NUnit.Framework;
using Moq;

namespace Wonga.Common.Testing.NUnit
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public abstract class ParallelizableTestFixtureFor<TSut> where TSut : class
    {
        private ConcurrentDictionary<Type, object> _mocks;
        private TestLocal<TSut> _sut;

        protected abstract TSut ConstructSystemUnderTest();
        protected TSut Sut => _sut.Value;

        protected Mock<TDependency> GetMock<TDependency>() where TDependency : class
        {
            Mock<TDependency> MockFactory() => new Mock<TDependency>();

            object WrapperFactory(Type t) => new TestLocal<Mock<TDependency>>(MockFactory);

            var wrapper = _mocks.GetOrAdd(typeof(TDependency), WrapperFactory) as TestLocal<Mock<TDependency>>;

            return wrapper?.Value;
        }

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _mocks = new ConcurrentDictionary<Type, object>();
            _sut = new TestLocal<TSut>(ConstructSystemUnderTest);

            TestFixtureSetUp();
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {
            TestFixtureTearDown();
        }

        [SetUp]
        public void TestSetUp()
        {
            SetUp();
        }

        [TearDown]
        public void TestTearDown()
        {
            TearDown();
        }

        protected TResult Act<TResult>(Func<TSut, TResult> act)
        {
            return act.Invoke(Sut);
        }

        protected void Act(Action<TSut> act)
        {
            act.Invoke(Sut);
        }
        protected virtual void TestFixtureSetUp()
        {
        }

        protected virtual void TestFixtureTearDown()
        {
        }

        protected virtual void SetUp()
        {
        }

        protected virtual void TearDown()
        {
        }
    }
}