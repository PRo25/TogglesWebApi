using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toggles.BusinessEntities;

namespace Toggles.BusinessRules.Contracts.Mocks
{
    public class MockOfITogglesLoader : Mock<ITogglesLoader>
    {
        private IList<Toggle> mockedToggles;

        public MockOfITogglesLoader(IList<Toggle> mockedToggles)
        {
            this.mockedToggles = mockedToggles;
            this.Initialize();
        }

        private void Initialize()
        {
            this.Setup(m => m.GetAll())
                .Returns(() => this.mockedToggles);
            this.Setup(m => m.GetById(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    Toggle toggle = this.mockedToggles.FirstOrDefault(t => t.Id == id);
                    return toggle;
                });
        }

        public void SetupToThrowException(Exception exception)
        {
            this.Setup(m => m.GetById(It.IsAny<Guid>())).Throws(exception);
        }
    }
}
