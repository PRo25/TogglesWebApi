using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Toggles.BusinessEntities;
using Toggles.Repositories.Contracts.Mocks;

namespace Toggles.Repositories.UnitTests
{
    [TestFixture]
    public class TogglesRepositoryTests : RepositoryTestsBase<TogglesRepository, Toggle>
    {
        private MockOfITogglesReadRepository mockOfITogglesReadRepository;

        [SetUp]
        public void SetupBeforeTest()
        {
            this.mockOfITogglesReadRepository = new MockOfITogglesReadRepository();
        }

        [TearDown]
        public void TearDownAfterTest()
        {
            this.mockOfITogglesReadRepository = null;
        }

        [Test]
        public void TestGetAll()
        {
            TogglesRepository togglesRepository = this.CreateRepository();

            IList<Toggle> result = togglesRepository.GetAll();

            Assert.IsNotNull(result);
            this.mockOfITogglesReadRepository.Verify(m => m.GetAll(), Times.Once);
        }

        protected override TogglesRepository CreateRepository()
        {
            var togglesRepository = new TogglesRepository(this.mockOfITogglesReadRepository.Object);
            return togglesRepository;
        }

        [Test]
        public void TestGetById()
        {
            TogglesRepository togglesRepository = this.CreateRepository();
            Guid toggleId = Guid.NewGuid();

            Toggle result = togglesRepository.GetById(toggleId);

            Assert.IsNotNull(result);
            this.mockOfITogglesReadRepository.Verify(m => m.GetById(toggleId), Times.Once);
        }

        [Test]
        public void TestHasAnyById()
        {
            TogglesRepository togglesRepository = this.CreateRepository();
            Guid toggleId = Guid.NewGuid();

            TestDelegate action = () => togglesRepository.HasAnyById(toggleId);

            Assert.DoesNotThrow(action);
            this.mockOfITogglesReadRepository.Verify(m => m.HasAnyById(toggleId), Times.Once);
        }

        [Test]
        public void TestHasAnyByCodeName()
        {
            TogglesRepository togglesRepository = this.CreateRepository();
            string toggleCodeName = "TestCodeName";

            TestDelegate action = () => togglesRepository.HasAnyByCodeName(toggleCodeName);

            Assert.DoesNotThrow(action);
            this.mockOfITogglesReadRepository.Verify(m => m.HasAnyByCodeName(toggleCodeName), Times.Once);
        }
    }
}
