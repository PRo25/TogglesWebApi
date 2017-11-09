using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Toggles.BusinessEntities;
using Toggles.BusinessRules.Contracts.Mocks;
using Toggles.BusinessRules.UnitTests.Entities;
using Toggles.Repositories.Contracts.Mocks;

namespace Toggles.BusinessRules.UnitTests
{
    [TestFixture]
    public class UpdateToggleCommandTests
    {
        private MockedTogglesCreator mockedTogglesCreator = new MockedTogglesCreator();

        [Test]
        public void TestExecute()
        {
            var mockOfITogglesUnitOfWork = new MockOfITogglesUnitOfWork();
            IList<Toggle> mockedToggles = this.mockedTogglesCreator.CreateList();
            var mockOfITogglesLoader = new MockOfITogglesLoader(mockedToggles);
            var updateToggleCommand = new UpdateToggleCommand(mockOfITogglesUnitOfWork.Object,
                mockOfITogglesLoader.Object);
            Toggle originalToggle = mockedToggles.First();
            Toggle updatedToggle = this.CreateMockedUpdatedToggle(originalToggle);

            TestDelegate action = () => updateToggleCommand.Execute(updatedToggle);

            Assert.DoesNotThrow(action);
            mockOfITogglesUnitOfWork.MockOfITogglesRepository.Verify(m => m.Update(updatedToggle), Times.Once);
            mockOfITogglesUnitOfWork.Verify(m => m.SaveChanges(), Times.Once);
        }

        private Toggle CreateMockedUpdatedToggle(Toggle originalToggle)
        {
            Toggle updatedToggle = this.mockedTogglesCreator.CreateToggle();
            updatedToggle.Id = originalToggle.Id;

            //this allows to test the update of a toggle value
            updatedToggle.Values.First().Id = originalToggle.Values.First().Id;

            return updatedToggle;
        }
    }
}
