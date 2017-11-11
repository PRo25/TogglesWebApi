using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Toggles.BusinessEntities;
using Toggles.BusinessEntities.Exceptions;
using Toggles.BusinessRules.Contracts.Mocks;
using TogglesWebApi.Controllers;

namespace TogglesWebApi.UnitTests.Controllers
{
    [TestFixture]
    public class TogglesControllerTests
    {
        [Test]
        public void TestGetAll()
        {
            var mockOfITogglesLoader = new MockOfITogglesLoader(new List<Toggle>());
            var togglesController = new TogglesController(mockOfITogglesLoader.Object, null, null, null, null);

            IEnumerable<Toggle> result = togglesController.GetAll();

            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetById()
        {
            var toggle = new Toggle();
            var mockOfITogglesLoader = new MockOfITogglesLoader(
                new List<Toggle>()
                {
                    toggle
                });
            var togglesController = new TogglesController(mockOfITogglesLoader.Object, null, null, null, null);

            IActionResult result = togglesController.GetById(toggle.Id);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void TestGetById_ReturnsNotFoundResultWhenEntityNotFoundException()
        {
            Guid toggleId = Guid.NewGuid();
            var mockOfITogglesLoader = new MockOfITogglesLoader(new List<Toggle>());
            mockOfITogglesLoader.SetupToThrowException(
                new EntityNotFoundException(typeof(Toggle), toggleId.ToString()));
            var togglesController = new TogglesController(mockOfITogglesLoader.Object, null, null, null, null);

            IActionResult result = togglesController.GetById(toggleId);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void TestGetById_ReturnsBadRequestResultWhenEntityValidationException()
        {
            Guid toggleId = Guid.NewGuid();
            var mockOfITogglesLoader = new MockOfITogglesLoader(new List<Toggle>());
            mockOfITogglesLoader.SetupToThrowException(
                new EntityValidationException(""));
            var togglesController = new TogglesController(mockOfITogglesLoader.Object, null, null, null, null);

            IActionResult result = togglesController.GetById(toggleId);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void TestGetByApplication()
        {
            var mockOfIToggleValuesLoader = new MockOfIToggleValuesLoader(new List<ToggleValue>());
            var togglesController = new TogglesController(null, mockOfIToggleValuesLoader.Object, null, null, null);

            IActionResult result = togglesController.GetByApplication("TestApp", "1.0");

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void TestPost()
        {
            var toggle = new Toggle();
            var mockOfICreateToggleCommand = new MockOfICreateToggleCommand();
            var togglesController = new TogglesController(null, null, mockOfICreateToggleCommand.Object, null, null);

            IActionResult result = togglesController.Post(toggle);

            Assert.IsInstanceOf<CreatedResult>(result);
        }

        [Test]
        public void TestPost_ReturnsNotFoundResultWhenEntityNotFoundException()
        {
            var toggle = new Toggle();
            var mockOfICreateToggleCommand = new MockOfICreateToggleCommand();
            mockOfICreateToggleCommand.SetupToThrowException(
                new EntityNotFoundException(typeof(Toggle), toggle.Id.ToString()));
            var togglesController = new TogglesController(null, null, mockOfICreateToggleCommand.Object, null, null);

            IActionResult result = togglesController.Post(toggle);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void TestPost_ReturnsBadRequestResultWhenEntityValidationException()
        {
            var toggle = new Toggle();
            var mockOfICreateToggleCommand = new MockOfICreateToggleCommand();
            mockOfICreateToggleCommand.SetupToThrowException(
                new EntityValidationException(""));
            var togglesController = new TogglesController(null, null, mockOfICreateToggleCommand.Object, null, null);

            IActionResult result = togglesController.Post(toggle);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void TestPost_ReturnsBadRequestResultWhenModelStateIsInvalid()
        {
            var toggle = new Toggle();
            var mockOfICreateToggleCommand = new MockOfICreateToggleCommand();
            var togglesController = new TogglesController(null, null, mockOfICreateToggleCommand.Object, null, null);
            this.ChangeModelStateToBeInvalid(togglesController);

            IActionResult result = togglesController.Post(toggle);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        private void ChangeModelStateToBeInvalid(Controller controller)
        {
            controller.ModelState.AddModelError("Test", "Test message");
        }

        [Test]
        public void TestPut()
        {
            var toggle = new Toggle();
            var mockOfIUpdateToggleCommand = new MockOfIUpdateToggleCommand();
            var togglesController = new TogglesController(null, null, null, mockOfIUpdateToggleCommand.Object, null);

            IActionResult result = togglesController.Put(toggle.Id, toggle);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void TestPut_ReturnsBadRequestResultWhenIdsAreNotEqual()
        {
            var toggle = new Toggle();
            var mockOfIUpdateToggleCommand = new MockOfIUpdateToggleCommand();
            var togglesController = new TogglesController(null, null, null, mockOfIUpdateToggleCommand.Object, null);

            IActionResult result = togglesController.Put(Guid.NewGuid(), toggle);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void TestPut_ReturnsNotFoundResultWhenEntityNotFoundException()
        {
            var toggle = new Toggle();
            var mockOfIUpdateToggleCommand = new MockOfIUpdateToggleCommand();
            mockOfIUpdateToggleCommand.SetupToThrowException(
                new EntityNotFoundException(typeof(Toggle), toggle.Id.ToString()));
            var togglesController = new TogglesController(null, null, null, mockOfIUpdateToggleCommand.Object, null);

            IActionResult result = togglesController.Put(toggle.Id, toggle);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void TestPut_ReturnsBadRequestResultWhenEntityValidationException()
        {
            var toggle = new Toggle();
            var mockOfIUpdateToggleCommand = new MockOfIUpdateToggleCommand();
            mockOfIUpdateToggleCommand.SetupToThrowException(
                new EntityValidationException(""));
            var togglesController = new TogglesController(null, null, null, mockOfIUpdateToggleCommand.Object, null);

            IActionResult result = togglesController.Put(toggle.Id, toggle);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void TestPut_ReturnsBadRequestResultWhenModelStateIsInvalid()
        {
            var toggle = new Toggle();
            var mockOfIUpdateToggleCommand = new MockOfIUpdateToggleCommand();
            var togglesController = new TogglesController(null, null, null, mockOfIUpdateToggleCommand.Object, null);
            this.ChangeModelStateToBeInvalid(togglesController);

            IActionResult result = togglesController.Put(toggle.Id, toggle);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void TestDelete()
        {
            var toggle = new Toggle();
            var mockOfIDeleteToggleCommand = new MockOfIDeleteToggleCommand();
            var togglesController = new TogglesController(null, null, null, null, mockOfIDeleteToggleCommand.Object);

            IActionResult result = togglesController.Delete(toggle.Id);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void TestDelete_ReturnsNotFoundResultWhenEntityNotFoundException()
        {
            var toggle = new Toggle();
            var mockOfIDeleteToggleCommand = new MockOfIDeleteToggleCommand();
            mockOfIDeleteToggleCommand.SetupToThrowException(
                new EntityNotFoundException(typeof(Toggle), toggle.Id.ToString()));
            var togglesController = new TogglesController(null, null, null, null, mockOfIDeleteToggleCommand.Object);

            IActionResult result = togglesController.Delete(toggle.Id);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void TestDelete_ReturnsBadRequestResultWhenEntityValidationException()
        {
            var toggle = new Toggle();
            var mockOfIDeleteToggleCommand = new MockOfIDeleteToggleCommand();
            mockOfIDeleteToggleCommand.SetupToThrowException(
                new EntityValidationException(""));
            var togglesController = new TogglesController(null, null, null, null, mockOfIDeleteToggleCommand.Object);

            IActionResult result = togglesController.Delete(toggle.Id);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
