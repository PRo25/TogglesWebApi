using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Toggles.BusinessEntities;
using Toggles.DataAccess.DbEntities;
using Toggles.DataAccess.Mocks;
using Toggles.Repositories.UnitTests.DbEntities;

namespace Toggles.Repositories.UnitTests
{
    [TestFixture]
    public class ToggleValuesReadRepositoryTests
    {
        private MockedToggleValueDbEntitiesCreator mockedToggleValueDbEntitiesCreator
            = new MockedToggleValueDbEntitiesCreator();

        [Test]
        public void TestGetByApplication_WhenHasValuesForApplication()
        {
            IList<ToggleValueDbEntity> mockedToggleValueDbEntities = this.CreateMockedToggleValueDbEntities();
            var mockOfTogglesDbContext = new MockOfTogglesDbContext(mockedToggleValueDbEntities);
            var toggleValuesReadRepository = new ToggleValuesReadRepository(mockOfTogglesDbContext.Object);
            ClientApplication clientApplication = this.CreateClientApplicationThatHasValues();

            IList<ToggleValue> result = toggleValuesReadRepository.GetByApplication(clientApplication);

            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(this.AreAllToggleIdsInResult(mockedToggleValueDbEntities, result));
        }

        private IList<ToggleValueDbEntity> CreateMockedToggleValueDbEntities()
        {
            return this.mockedToggleValueDbEntitiesCreator.CreateList();
        }

        private ClientApplication CreateClientApplicationThatHasValues()
        {
            ToggleValueDbEntity toggleValueDbEntity =
                this.mockedToggleValueDbEntitiesCreator.CreateToggleValueDbEntity();
            var clientApplication = new ClientApplication()
            {
                CodeName = toggleValueDbEntity.ApplicationCodeName,
                Version = toggleValueDbEntity.ApplicationVersion
            };
            return clientApplication;
        }

        private bool AreAllToggleIdsInResult(IList<ToggleValueDbEntity> mockedToggleValueDbEntities,
            IList<ToggleValue> result)
        {
            return mockedToggleValueDbEntities.All(
                dbe => result.Any(r => r.ToggleId == dbe.ToggleId));
        }

        [Test]
        public void TestGetByApplication_ReturnsOnlyGlobalValuesWhenApplicationDoesNotHaveValues()
        {
            IList<ToggleValueDbEntity> mockedToggleValueDbEntities = this.CreateMockedToggleValueDbEntities();
            var mockOfTogglesDbContext = new MockOfTogglesDbContext(mockedToggleValueDbEntities);
            var toggleValuesReadRepository = new ToggleValuesReadRepository(mockOfTogglesDbContext.Object);
            ClientApplication clientApplication = this.CreateClientApplicationThatDoesNotHaveValues();

            IList<ToggleValue> result = toggleValuesReadRepository.GetByApplication(clientApplication);

            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(AreOnlyGlobalValuesInResult(mockedToggleValueDbEntities, result));
            Assert.IsTrue(AreAllGlobalValuesInResult(mockedToggleValueDbEntities, result));
        }

        private ClientApplication CreateClientApplicationThatDoesNotHaveValues()
        {
            return new ClientApplication()
            {
                CodeName = Guid.NewGuid().ToString(),
                Version = Guid.NewGuid().ToString()
            };
        }

        private bool AreOnlyGlobalValuesInResult(IList<ToggleValueDbEntity> mockedToggleValueDbEntities,
            IList<ToggleValue> result)
        {
            IEnumerable<Guid> globalValueIds = this.GetGlobalValueIds(mockedToggleValueDbEntities);
            return result.All(r => globalValueIds.Contains(r.Id));
        }

        private IEnumerable<Guid> GetGlobalValueIds(IList<ToggleValueDbEntity> mockedToggleValueDbEntities)
        {
            return mockedToggleValueDbEntities
               .Where(dbe => dbe.ApplicationCodeName == ToggleValueDbEntity.GLOBAL_APPLICATION_CODE_NAME)
               .Select(dbe => dbe.Id);
        }

        private bool AreAllGlobalValuesInResult(IList<ToggleValueDbEntity> mockedToggleValueDbEntities,
            IList<ToggleValue> result)
        {
            IEnumerable<Guid> globalValueIds = this.GetGlobalValueIds(mockedToggleValueDbEntities);
            return globalValueIds.All(id => result.Any(r => r.Id == id));
        }
    }
}
