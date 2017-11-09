using Moq;
using System.Collections.Generic;
using Toggles.BusinessEntities;

namespace Toggles.Repositories.Contracts.Mocks
{
    public class MockOfIToggleValuesReadRepository : Mock<IToggleValuesReadRepository>
    {
        public MockOfIToggleValuesReadRepository()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.SetupGetByApplication();
        }

        private void SetupGetByApplication()
        {
            this.Setup(m => m.GetByApplication(It.IsAny<ClientApplication>()))
                .Returns((ClientApplication clientApplication) =>
                {
                    IList<ToggleValue> toggleValues = new List<ToggleValue>()
                    {
                        new ToggleValue(),
                        new ToggleValue()
                    };
                    return toggleValues;
                });
        }
    }
}
