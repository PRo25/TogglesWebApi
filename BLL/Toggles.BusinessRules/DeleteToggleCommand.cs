using System;
using Toggles.BusinessEntities;
using Toggles.BusinessRules.Contracts;
using Toggles.Repositories.Contracts;

namespace Toggles.BusinessRules
{
    public class DeleteToggleCommand : IDeleteToggleCommand
    {
        private ITogglesUnitOfWork unitOfWork;
        private ITogglesLoader togglesLoader;

        public DeleteToggleCommand(ITogglesUnitOfWork togglesUnitOfWork, ITogglesLoader togglesLoader)
        {
            this.unitOfWork = togglesUnitOfWork;
            this.togglesLoader = togglesLoader;
        }

        public void Execute(Guid toggleId)
        {
            Toggle toggle = this.togglesLoader.GetById(toggleId);
            this.unitOfWork.TogglesRepository.Delete(toggle);
            this.unitOfWork.SaveChanges();
        }
    }
}
