using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Toggles.BusinessEntities;

namespace Toggles.Repositories.Contracts.Mocks
{
    public class MockOfIClientApplicationToggleValuesRepository : Mock<IClientApplicationToggleValuesRepository>
    {
        public int NrOfChanges { get; private set; }

        public MockOfIClientApplicationToggleValuesRepository()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.Setup(m => m.Add(It.IsAny<ClientApplicationToggleValue>()))
                .Callback(() =>
                {
                    this.NrOfChanges++;
                });
        }
    }
}
