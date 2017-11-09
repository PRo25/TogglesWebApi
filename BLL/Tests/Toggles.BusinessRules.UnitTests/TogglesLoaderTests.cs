using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Toggles.BusinessEntities;
using Toggles.Repositories.Contracts.Mocks;

namespace Toggles.BusinessRules.UnitTests
{
    [TestFixture]
    public class TogglesLoaderTests
    {
        [Test]
        public void TestGetAll()
        {
            var mockOfITogglesReadRepository = new MockOfITogglesReadRepository();
            var togglesLoader = new TogglesLoader(mockOfITogglesReadRepository.Object);

            IList<Toggle> result = togglesLoader.GetAll();

            Assert.IsTrue(result.Count > 0);
            mockOfITogglesReadRepository.Verify(m => m.GetAll(), Times.Once);
        }

        [Test]
        public void TestGetById()
        {
            var mockOfITogglesReadRepository = new MockOfITogglesReadRepository();
            var togglesLoader = new TogglesLoader(mockOfITogglesReadRepository.Object);

            Guid toggleId = Guid.NewGuid();
            Toggle result = togglesLoader.GetById(toggleId);

            Assert.IsNotNull(result);
            mockOfITogglesReadRepository.Verify(m => m.GetById(toggleId), Times.Once);
        }
    }
}
