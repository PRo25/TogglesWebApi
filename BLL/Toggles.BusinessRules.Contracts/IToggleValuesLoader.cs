using System.Collections.Generic;
using Toggles.BusinessEntities;

namespace Toggles.BusinessRules.Contracts
{
    public interface IToggleValuesLoader
    {
        IList<ToggleValue> GetByApplication(Application application);
    }
}
