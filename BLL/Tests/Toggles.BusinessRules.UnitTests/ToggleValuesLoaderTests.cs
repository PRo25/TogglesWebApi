using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Toggles.BusinessEntities;
using Toggles.Repositories.Contracts.Mocks;

namespace Toggles.BusinessRules.UnitTests
{
    [TestFixture]
    public class ToggleValuesLoaderTests
    {
        [Test]
        public void TestGetByApplication()
        {
            var mockOfIToggleValuesReadRepository = new MockOfIToggleValuesReadRepository();
            var toggleValuesLoader = new ToggleValuesLoader(mockOfIToggleValuesReadRepository.Object);
            var clientApplication = new ClientApplication();

            IList<ToggleValue> result = toggleValuesLoader.GetByApplication(clientApplication);

            Assert.IsTrue(result.Count > 0);
            mockOfIToggleValuesReadRepository.Verify(m => m.GetByApplication(clientApplication), Times.Once);
        }
    }
}
