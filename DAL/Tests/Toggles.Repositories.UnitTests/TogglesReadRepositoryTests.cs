using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Toggles.BusinessEntities;
using Toggles.BusinessEntities.Exceptions;
using Toggles.DataAccess.DbEntities;
using Toggles.DataAccess.Mocks;
using Toggles.Repositories.UnitTests.DbEntities;

namespace Toggles.Repositories.UnitTests
{
    [TestFixture]
    public class TogglesReadRepositoryTests
    {
        [Test]
        public void TestGetAll_WhenHasItems()
        {
            IList<ToggleDbEntity> mockedToggleDbEntities = this.CreateMockedToggleDbEntities();
            var mockOfTogglesDbContext = new MockOfTogglesDbContext(mockedToggleDbEntities);
            var togglesReadRepository = new TogglesReadRepository(mockOfTogglesDbContext.Object);

            IList<Toggle> result = togglesReadRepository.GetAll();

            Assert.IsTrue(result.Count > 0);
        }

        private IList<ToggleDbEntity> CreateMockedToggleDbEntities()
        {
            var mockedToggleDbEntitiesCreator = new MockedToggleDbEntitiesCreator();
            return mockedToggleDbEntitiesCreator.CreateList();
        }

        [Test]
        public void TestGetAll_WhenEmpty()
        {
            var mockOfTogglesDbContext = new MockOfTogglesDbContext(new List<ToggleDbEntity>());
            var togglesReadRepository = new TogglesReadRepository(mockOfTogglesDbContext.Object);

            IList<Toggle> result = togglesReadRepository.GetAll();

            Assert.IsTrue(result.Count == 0);
        }

        [Test]
        public void TestGetById()
        {
            IList<ToggleDbEntity> mockedToggleDbEntities = this.CreateMockedToggleDbEntities();
            var mockOfTogglesDbContext = new MockOfTogglesDbContext(mockedToggleDbEntities);
            var togglesReadRepository = new TogglesReadRepository(mockOfTogglesDbContext.Object);
            ToggleDbEntity targetToggleDbEntity = mockedToggleDbEntities.First();

            Toggle result = togglesReadRepository.GetById(targetToggleDbEntity.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id == targetToggleDbEntity.Id && result.CodeName == targetToggleDbEntity.CodeName);
        }

        [Test]
        public void TestGetById_FailsWhenEntityNotFound()
        {
            IList<ToggleDbEntity> mockedToggleDbEntities = this.CreateMockedToggleDbEntities();
            var mockOfTogglesDbContext = new MockOfTogglesDbContext(mockedToggleDbEntities);
            var togglesReadRepository = new TogglesReadRepository(mockOfTogglesDbContext.Object);
            Guid nonExistingToggleId = Guid.NewGuid();

            TestDelegate action = () => togglesReadRepository.GetById(nonExistingToggleId);

            EntityNotFoundException exc = Assert.Throws<EntityNotFoundException>(action);
            Assert.IsTrue(exc.Message.Contains(nonExistingToggleId.ToString()));
        }

        [Test]
        public void TestHasAnyById_ReturnsTrueWhenEntityFound()
        {
            this.TestActionReturnsTrueWhenEntityFound(
                (togglesReadRepository, toggleDbEntity) => togglesReadRepository.HasAnyById(toggleDbEntity.Id));
        }

        private void TestActionReturnsTrueWhenEntityFound(Func<TogglesReadRepository, ToggleDbEntity, bool> testAction)
        {
            IList<ToggleDbEntity> mockedToggleDbEntities = this.CreateMockedToggleDbEntities();
            var mockOfTogglesDbContext = new MockOfTogglesDbContext(mockedToggleDbEntities);
            var togglesReadRepository = new TogglesReadRepository(mockOfTogglesDbContext.Object);
            ToggleDbEntity targetToggleDbEntity = mockedToggleDbEntities.First();

            bool result = testAction(togglesReadRepository, targetToggleDbEntity);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestHasAnyById_ReturnsFalseWhenEntityNotFound()
        {
            this.TestActionReturnsFalseWhenEntityNotFound(
                (togglesReadRepository) =>
                {
                    Guid nonExistingToggleId = Guid.NewGuid();
                    return togglesReadRepository.HasAnyById(nonExistingToggleId);
                });
        }

        private void TestActionReturnsFalseWhenEntityNotFound(
            Func<TogglesReadRepository, bool> testAction)
        {
            IList<ToggleDbEntity> mockedToggleDbEntities = this.CreateMockedToggleDbEntities();
            var mockOfTogglesDbContext = new MockOfTogglesDbContext(mockedToggleDbEntities);
            var togglesReadRepository = new TogglesReadRepository(mockOfTogglesDbContext.Object);

            bool result = testAction(togglesReadRepository);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestHasAnyByCodeName_ReturnsTrueWhenEntityFound()
        {
            this.TestActionReturnsTrueWhenEntityFound(
                (togglesReadRepository, toggleDbEntity) => togglesReadRepository.HasAnyByCodeName(toggleDbEntity.CodeName));
        }

        [Test]
        public void TestHasAnyByCodeName_ReturnsFalseWhenEntityNotFound()
        {
            this.TestActionReturnsFalseWhenEntityNotFound(
                (togglesReadRepository) =>
                {
                    string nonExistingCodeName = Guid.NewGuid().ToString();
                    return togglesReadRepository.HasAnyByCodeName(nonExistingCodeName);
                });
        }
    }
}
