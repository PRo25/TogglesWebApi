using System.Collections.Generic;
using Toggles.BusinessEntities;

namespace Toggles.Repositories.Contracts
{
    public interface IToggleValuesReadRepository
    {
        IList<ToggleValue> GetByApplication(Application application);
    }
}
