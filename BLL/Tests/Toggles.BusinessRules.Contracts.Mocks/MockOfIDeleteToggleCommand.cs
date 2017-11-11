using Moq;
using System;

namespace Toggles.BusinessRules.Contracts.Mocks
{
    public class MockOfIDeleteToggleCommand : Mock<IDeleteToggleCommand>
    {
        public MockOfIDeleteToggleCommand()
        {
        }

        public void SetupToThrowException(Exception exception)
        {
            this.Setup(m => m.Execute(It.IsAny<Guid>())).Throws(exception);
        }
    }
}
