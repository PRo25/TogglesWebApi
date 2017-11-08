using System;
using Toggles.BusinessEntities;
using Toggles.BusinessEntities.Exceptions;
using Toggles.BusinessRules.Contracts;
using Toggles.Repositories.Contracts;

namespace Toggles.BusinessRules
{
    public class CreateToggleCommand : ICreateToggleCommand
    {
        private ITogglesUnitOfWork unitOfWork;

        public CreateToggleCommand(ITogglesUnitOfWork togglesUnitOfWork)
        {
            this.unitOfWork = togglesUnitOfWork;
        }

        public void Execute(Toggle toggle)
        {
            this.ValidateIfCanExecute(toggle);
            this.unitOfWork.TogglesRepository.Add(toggle);
            foreach (ApplicationToggleValue appToggleValue in toggle.Values)
            {
                appToggleValue.ToggleId = toggle.Id;
                this.unitOfWork.ApplicationToggleValuesRepository.Add(appToggleValue);
            }
            this.unitOfWork.SaveChanges();
        }

        private void ValidateIfCanExecute(Toggle toggle)
        {
            this.ValidateIfAlreadyExistsWithSameId(toggle);
            this.ValidateIfAlreadyExistsWithSameCodeName(toggle);
        }

        private void ValidateIfAlreadyExistsWithSameId(Toggle toggle)
        {
            bool alreadyExists = this.unitOfWork.TogglesRepository.HasAnyById(toggle.Id);
            if (alreadyExists)
            {
                throw new EntityValidationException($"Cannot create Toggle because there's already a Toggle with" +
                    $" the Id '{toggle.Id}'.");
            }
        }

        private void ValidateIfAlreadyExistsWithSameCodeName(Toggle toggle)
        {
            bool alreadyExists = this.unitOfWork.TogglesRepository.HasAnyByCodeName(toggle.CodeName);
            if (alreadyExists)
            {
                throw new EntityValidationException($"Cannot create Toggle because there's already a Toggle with" +
                    $" the CodeName '{toggle.CodeName}'.");
            }
        }
    }
}
