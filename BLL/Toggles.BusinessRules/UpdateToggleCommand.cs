using System;
using System.Collections.Generic;
using System.Linq;
using Toggles.BusinessEntities;
using Toggles.BusinessRules.Contracts;
using Toggles.Repositories.Contracts;

namespace Toggles.BusinessRules
{
    public class UpdateToggleCommand : IUpdateToggleCommand
    {
        private ITogglesUnitOfWork unitOfWork;
        private ITogglesLoader togglesLoader;
        private Toggle originalExistingToggle;
        private Toggle updatedToggle;

        public UpdateToggleCommand(ITogglesUnitOfWork togglesUnitOfWork, ITogglesLoader togglesLoader)
        {
            this.unitOfWork = togglesUnitOfWork;
            this.togglesLoader = togglesLoader;
        }

        public void Execute(Toggle toggle)
        {
            this.updatedToggle = toggle;
            this.originalExistingToggle = this.togglesLoader.GetById(this.updatedToggle.Id);
            this.unitOfWork.TogglesRepository.Update(this.updatedToggle);
            this.AddOrUpdateToggleValues();
            this.DeleteExistingToggleValues();
            this.unitOfWork.SaveChanges();
        }

        private void AddOrUpdateToggleValues()
        {
            foreach (ClientApplicationToggleValue appToggleValue in this.updatedToggle.Values)
            {
                appToggleValue.ToggleId = this.updatedToggle.Id;
                if (this.ExistsWithSameId(appToggleValue))
                {
                    this.unitOfWork.ClientApplicationToggleValuesRepository.Update(appToggleValue);
                }
                else
                {
                    this.unitOfWork.ClientApplicationToggleValuesRepository.Add(appToggleValue);
                }
            }
        }

        private bool ExistsWithSameId(ClientApplicationToggleValue appToggleValue)
        {
            return this.originalExistingToggle.Values.Any(tv => tv.Id == appToggleValue.Id);
        }

        private void DeleteExistingToggleValues()
        {
            IEnumerable<ClientApplicationToggleValue> toggleValuesToDelete =
                from originalToggleValue in this.originalExistingToggle.Values
                where !this.updatedToggle.Values.Any(tv => tv.Id == originalToggleValue.Id)
                select originalToggleValue;
            foreach (ClientApplicationToggleValue toggleValueToDelete in toggleValuesToDelete)
            {
                this.unitOfWork.ClientApplicationToggleValuesRepository.Delete(toggleValueToDelete);
            }
        }
    }
}
