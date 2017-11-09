using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Toggles.BusinessEntities;
using Toggles.BusinessRules.Contracts.Mocks;
using Toggles.BusinessRules.UnitTests.Entities;
using Toggles.Repositories.Contracts.Mocks;

namespace Toggles.BusinessRules.UnitTests
{
    [TestFixture]
    public class DeleteToggleCommandTests
    {
        private MockedTogglesCreator mockedTogglesCreator = new MockedTogglesCreator();

        [Test]
        public void TestExecute()
        {
            var mockOfITogglesUnitOfWork = new MockOfITogglesUnitOfWork();
            IList<Toggle> mockedToggles = this.mockedTogglesCreator.CreateList();
            var mockOfITogglesLoader = new MockOfITogglesLoader(mockedToggles);
            var deleteToggleCommand = new DeleteToggleCommand(mockOfITogglesUnitOfWork.Object,
                mockOfITogglesLoader.Object);
            Toggle toggleToDelete = mockedToggles.First();

            TestDelegate action = () => deleteToggleCommand.Execute(toggleToDelete.Id);

            Assert.DoesNotThrow(action);
            mockOfITogglesUnitOfWork.MockOfITogglesRepository.Verify(m => m.Delete(toggleToDelete), Times.Once);
            mockOfITogglesUnitOfWork.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
