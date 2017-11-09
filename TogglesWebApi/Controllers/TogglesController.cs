using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toggles.BusinessEntities;
using Toggles.BusinessRules.Contracts;
using Toggles.BusinessEntities.Exceptions;
using TogglesWebApi.Results;

namespace TogglesWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TogglesController : Controller
    {
        private ITogglesLoader togglesLoader;
        private IToggleValuesLoader toggleValuesLoader;
        private ICreateToggleCommand addToggleCommand;
        private IUpdateToggleCommand updateToggleCommand;
        private IDeleteToggleCommand deleteToggleCommand;

        public TogglesController(ITogglesLoader togglesLoader,
            IToggleValuesLoader toggleValuesLoader,
            ICreateToggleCommand addToggleCommand,
            IUpdateToggleCommand updateToggleCommand,
            IDeleteToggleCommand deleteToggleCommand)
        {
            this.togglesLoader = togglesLoader;
            this.toggleValuesLoader = toggleValuesLoader;
            this.addToggleCommand = addToggleCommand;
            this.updateToggleCommand = updateToggleCommand;
            this.deleteToggleCommand = deleteToggleCommand;
        }

        [HttpGet]
        public IEnumerable<Toggle> GetAll()
        {
            IEnumerable<Toggle> result = this.togglesLoader.GetAll();
            return result;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            IActionResult result = this.TryToExecuteActionOrReturnErrorResult(
                () => this.GetToggleById(id));
            return result;
        }

        private IActionResult TryToExecuteActionOrReturnErrorResult(Func<IActionResult> action)
        {
            IActionResult result = null;
            try
            {
                result = this.ExecuteActionIfModelStateIsValidOrReturnBadRequest(action);
            }
            catch (EntityNotFoundException notFoundExc)
            {
                result = this.CreateNotFoundResult(notFoundExc.Message);
            }
            catch (EntityValidationException validationExc)
            {
                result = this.CreateBadRequestResult(validationExc.Message);
            }
            return result;
        }

        private IActionResult ExecuteActionIfModelStateIsValidOrReturnBadRequest(Func<IActionResult> action)
        {
            IActionResult result = null;
            if (!this.ModelState.IsValid)
            {
                result = BadRequest(this.ModelState);
            }
            else
            {
                result = action();
            }
            return result;
        }

        private IActionResult CreateNotFoundResult(string message)
        {
            ErrorResult errorResult = new ErrorResult(message);
            return NotFound(errorResult);
        }

        private IActionResult CreateBadRequestResult(string message)
        {
            ErrorResult error = new ErrorResult(message);
            return BadRequest(error);
        }

        private IActionResult GetToggleById(Guid id)
        {
            Toggle result = this.togglesLoader.GetById(id);
            return Ok(result);
        }

        [HttpGet("ByApp/{applicationCodeName}/{applicationVersion}")]
        public IActionResult GetByApplication(string applicationCodeName, string applicationVersion)
        {
            ClientApplication application = new ClientApplication()
            {
                CodeName = applicationCodeName,
                Version = applicationVersion
            };
            IEnumerable<ToggleValue> result = this.toggleValuesLoader.GetByApplication(application);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Toggle toggle)
        {
            IActionResult result = this.TryToExecuteActionOrReturnErrorResult(
                () => this.CreateToggle(toggle));
            return result;
        }

        private IActionResult CreateToggle(Toggle toggle)
        {
            this.addToggleCommand.Execute(toggle);
            return Created("", toggle);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Toggle toggle)
        {
            IActionResult result = this.TryToExecuteActionOrReturnErrorResult(
                () => this.UpdateToggle(id, toggle));
            return result;
        }

        private IActionResult UpdateToggle(Guid id, Toggle toggle)
        {
            IActionResult result;
            if (id != toggle.Id)
            {
                result = this.CreateBadRequestResult(
                    "The ID in the request body cannot be different than the path ID.");
            }
            else
            {
                this.updateToggleCommand.Execute(toggle);
                result = Ok(toggle);
            }
            return result;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            IActionResult result = this.TryToExecuteActionOrReturnErrorResult(
                () => this.DeleteToggle(id));
            return result;
        }

        private IActionResult DeleteToggle(Guid id)
        {
            this.deleteToggleCommand.Execute(id);
            return NoContent();
        }
    }
}
