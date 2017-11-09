using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Toggles.BusinessEntities;
using Toggles.Repositories.Contracts.Mocks;
using Toggles.BusinessEntities.Exceptions;
using Toggles.BusinessRules.UnitTests.Entities;

namespace Toggles.BusinessRules.UnitTests
{
    [TestFixture]
    public class CreateToggleCommandTests
    {
        private MockedTogglesCreator mockedTogglesCreator = new MockedTogglesCreator();

        [Test]
        public void TestExecute()
        {
            MockOfITogglesUnitOfWork mockOfITogglesUnitOfWork = new MockOfITogglesUnitOfWork();
            CreateToggleCommand createToggleCommand = new CreateToggleCommand(mockOfITogglesUnitOfWork.Object);
            Toggle toggle = this.CreateMockedToggle();

            TestDelegate action = () => createToggleCommand.Execute(toggle);

            Assert.DoesNotThrow(action);
            mockOfITogglesUnitOfWork.Verify(m => m.SaveChanges(), Times.Once);
        }

        private Toggle CreateMockedToggle()
        {
            Toggle toggle = this.mockedTogglesCreator.CreateToggle();
            return toggle;
        }

        [Test]
        public void TestExecute_FailsWhenIdAlreadyExists()
        {
            MockOfITogglesUnitOfWork mockOfITogglesUnitOfWork = new MockOfITogglesUnitOfWork();
            mockOfITogglesUnitOfWork.MockOfITogglesRepository.SetupHasAnyByIdToReturnTrue();
            CreateToggleCommand createToggleCommand = new CreateToggleCommand(mockOfITogglesUnitOfWork.Object);
            Toggle toggle = this.CreateMockedToggle();

            TestDelegate action = () => createToggleCommand.Execute(toggle);

            Assert.Throws<EntityValidationException>(action);
        }

        [Test]
        public void TestExecute_FailsWhenCodeNameAlreadyExists()
        {
            MockOfITogglesUnitOfWork mockOfITogglesUnitOfWork = new MockOfITogglesUnitOfWork();
            mockOfITogglesUnitOfWork.MockOfITogglesRepository.SetupHasAnyByCodeNameToReturnTrue();
            CreateToggleCommand createToggleCommand = new CreateToggleCommand(mockOfITogglesUnitOfWork.Object);
            Toggle toggle = this.CreateMockedToggle();

            TestDelegate action = () => createToggleCommand.Execute(toggle);

            Assert.Throws<EntityValidationException>(action);
        }
    }
}
