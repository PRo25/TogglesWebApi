using Moq;
using System;
using Toggles.BusinessEntities;

namespace Toggles.Repositories.Contracts.Mocks
{
    public class MockOfITogglesRepository : RepositoryMock<ITogglesRepository, Toggle>
    {
        public MockOfITogglesRepository()
            : base()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.Setup(m => m.HasAnyById(It.IsAny<Guid>())).Returns(false);
            this.Setup(m => m.HasAnyByCodeName(It.IsAny<string>())).Returns(false);
        }

        public void SetupHasAnyByIdToReturnTrue()
        {
            this.Setup(m => m.HasAnyById(It.IsAny<Guid>())).Returns(true);
        }

        public void SetupHasAnyByCodeNameToReturnTrue()
        {
            this.Setup(m => m.HasAnyByCodeName(It.IsAny<string>())).Returns(true);
        }
    }
}
