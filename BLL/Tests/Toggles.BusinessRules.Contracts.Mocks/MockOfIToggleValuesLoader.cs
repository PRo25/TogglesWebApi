using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toggles.BusinessEntities;

namespace Toggles.BusinessRules.Contracts.Mocks
{
    public class MockOfIToggleValuesLoader : Mock<IToggleValuesLoader>
    {
        private IList<ToggleValue> mockedToggleValues;

        public MockOfIToggleValuesLoader(IList<ToggleValue> mockedToggleValues)
        {
            this.mockedToggleValues = mockedToggleValues;
            this.Initialize();
        }

        private void Initialize()
        {
            this.Setup(m => m.GetByApplication(It.IsAny<ClientApplication>()))
                .Returns(() => this.mockedToggleValues);
        }

        public void SetupToThrowException(Exception exception)
        {
            this.Setup(m => m.GetByApplication(It.IsAny<ClientApplication>())).Throws(exception);
        }
    }
}
