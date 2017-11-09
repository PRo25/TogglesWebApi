using Moq;
using System;
using System.Collections.Generic;
using Toggles.BusinessEntities;

namespace Toggles.Repositories.Contracts.Mocks
{
    public class MockOfITogglesReadRepository : Mock<ITogglesReadRepository>
    {
        public MockOfITogglesReadRepository()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.SetupGetAll();
            this.SetupGetById();
        }

        private void SetupGetAll()
        {
            this.Setup(m => m.GetAll())
                .Returns(() =>
                {
                    IList<Toggle> toggles = new List<Toggle>()
                    {
                        new Toggle(),
                        new Toggle()
                    };
                    return toggles;
                });
        }

        private void SetupGetById()
        {
            this.Setup(m => m.GetById(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    Toggle toggle = new Toggle()
                    {
                        Id = id
                    };
                    return toggle;
                });
        }
    }
}
