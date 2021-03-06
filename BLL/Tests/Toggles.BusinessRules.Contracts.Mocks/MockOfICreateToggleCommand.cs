﻿using Moq;
using System;
using Toggles.BusinessEntities;

namespace Toggles.BusinessRules.Contracts.Mocks
{
    public class MockOfICreateToggleCommand : Mock<ICreateToggleCommand>
    {
        public MockOfICreateToggleCommand()
        {
        }

        public void SetupToThrowException(Exception exception)
        {
            this.Setup(m => m.Execute(It.IsAny<Toggle>())).Throws(exception);
        }
    }
}
