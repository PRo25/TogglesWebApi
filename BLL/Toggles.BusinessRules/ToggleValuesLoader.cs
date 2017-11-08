using System;
using System.Collections.Generic;
using System.Text;
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

        public IList<ToggleValue> GetByApplication(Application application)
        {
            IList<ToggleValue> result = this.toggleValuesReadRepository.GetByApplication(application);
            return result;
        }
    }
}
