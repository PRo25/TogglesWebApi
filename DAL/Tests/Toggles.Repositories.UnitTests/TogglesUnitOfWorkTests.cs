using NUnit.Framework;
using System.Collections.Generic;
using Toggles.DataAccess.DbEntities;
using Toggles.DataAccess.Mocks;
using Toggles.Repositories.Contracts.Mocks;
using Toggles.Repositories.UnitTests.DbEntities;
using System;
using Toggles.BusinessEntities;
using System.Linq;

namespace Toggles.Repositories.UnitTests
{
    [TestFixture]
    public class TogglesUnitOfWorkTests
    {
        private MockedToggleDbEntitiesCreator mockedToggleDbEntitiesCreator = new MockedToggleDbEntitiesCreator();

        [Test]
        public void TestSaveChanges()
        {
            int targetNumberOfChanges = 0;
            IList<ToggleDbEntity> mockedToggleDbEntities = this.mockedToggleDbEntitiesCreator.CreateList();
            var mockOfTogglesDbContext = new MockOfTogglesDbContext(mockedToggleDbEntities);
            var mockOfITogglesRepository = new MockOfITogglesRepository();
            var mockOfIClientApplicationToggleValuesRepository = new MockOfIClientApplicationToggleValuesRepository();
            targetNumberOfChanges += this.SetupTogglesChanges(mockOfITogglesRepository, mockedToggleDbEntities);
            targetNumberOfChanges += this.SetupToggleValuesChanges(mockOfIClientApplicationToggleValuesRepository,
                mockedToggleDbEntities);
            var unitOfWork = new TogglesUnitOfWork(mockOfTogglesDbContext.Object, mockOfITogglesRepository.Object,
                mockOfIClientApplicationToggleValuesRepository.Object);

            int numberOfSavedChanges = unitOfWork.SaveChanges();

            Assert.IsTrue(targetNumberOfChanges == numberOfSavedChanges);
        }

        private int SetupTogglesChanges(MockOfITogglesRepository mockOfITogglesRepository,
            IList<ToggleDbEntity> mockedToggleDbEntitiesInDbContext)
        {
            ToggleDbEntity updatedToggleDbEntity = mockedToggleDbEntitiesInDbContext[0];
            ToggleDbEntity deletedToggleDbEntity = mockedToggleDbEntitiesInDbContext[1];
            Toggle addedToggle = new Toggle();
            Toggle updatedToggle = this.ConvertToBusinessEntity(updatedToggleDbEntity);
            Toggle deletedToggle = this.ConvertToBusinessEntity(deletedToggleDbEntity);
            mockOfITogglesRepository.Object.Add(addedToggle);
            mockOfITogglesRepository.Object.Update(updatedToggle);
            mockOfITogglesRepository.Object.Delete(deletedToggle);
            return mockOfITogglesRepository.NumberOfChanges;
        }

        private Toggle ConvertToBusinessEntity(ToggleDbEntity updatedToggleDbEntity)
        {
            var toggle = new Toggle()
            {
                Id = updatedToggleDbEntity.Id
            };
            return toggle;
        }

        private int SetupToggleValuesChanges(
            MockOfIClientApplicationToggleValuesRepository mockOfIClientApplicationToggleValuesRepository,
            IList<ToggleDbEntity> mockedToggleDbEntitiesInDbContext)
        {
            IList<ToggleValueDbEntity> mockedToggleValueDbEntities =
                mockedToggleDbEntitiesInDbContext[0].Values.ToList();
            ClientApplicationToggleValue updatedToggleValue =
                this.ConvertToBusinessEntity(mockedToggleValueDbEntities[0]);
            ClientApplicationToggleValue deletedToggleValue =
                this.ConvertToBusinessEntity(mockedToggleValueDbEntities[1]);
            mockOfIClientApplicationToggleValuesRepository.Object.Update(updatedToggleValue);
            mockOfIClientApplicationToggleValuesRepository.Object.Delete(deletedToggleValue);
            return mockOfIClientApplicationToggleValuesRepository.NumberOfChanges;
        }

        private ClientApplicationToggleValue ConvertToBusinessEntity(ToggleValueDbEntity toggleValueDbEntity)
        {
            var toggleValue = new ClientApplicationToggleValue()
            {
                Id = toggleValueDbEntity.Id,
                ToggleId = toggleValueDbEntity.ToggleId,
                Value = toggleValueDbEntity.Value,
                Application = new ClientApplication()
                {
                    CodeName = toggleValueDbEntity.ApplicationCodeName,
                    Version = toggleValueDbEntity.ApplicationVersion
                }
            };
            return toggleValue;
        }

        [Test]
        public void TestSaveChanges_WhenDoesNotHaveChanges()
        {
            var mockOfTogglesDbContext = new MockOfTogglesDbContext();
            var mockOfITogglesRepository = new MockOfITogglesRepository();
            var mockOfIClientApplicationToggleValuesRepository = new MockOfIClientApplicationToggleValuesRepository();
            var unitOfWork = new TogglesUnitOfWork(mockOfTogglesDbContext.Object, mockOfITogglesRepository.Object,
                mockOfIClientApplicationToggleValuesRepository.Object);

            int numberOfSavedChanges = unitOfWork.SaveChanges();

            Assert.IsTrue(numberOfSavedChanges == 0);
        }
    }
}
