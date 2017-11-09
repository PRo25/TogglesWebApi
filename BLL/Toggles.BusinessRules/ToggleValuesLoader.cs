using System.Collections.Generic;
using Toggles.BusinessEntities;
using Toggles.BusinessRules.Contracts;
using Toggles.Repositories.Contracts;

namespace Toggles.BusinessRules
{
    public class ToggleValuesLoader : IToggleValuesLoader
    {
        private IToggleValuesReadRepository toggleValuesReadRepository;

        public ToggleValuesLoader(IToggleValuesReadRepository toggleValuesReadRepository)
        {
            this.toggleValuesReadRepository = toggleValuesReadRepository;
        }

        public IList<ToggleValue> GetByApplication(ClientApplication application)
        {
            IList<ToggleValue> result = this.toggleValuesReadRepository.GetByApplication(application);
            return result;
        }
    }
}
