using System;
using Toggles.BusinessEntities;
using Toggles.Repositories.Contracts;

namespace Toggles.Repositories
{
    public class ApplicationToggleValuesRepository : RepositoryBase<ApplicationToggleValue>,
        IApplicationToggleValuesRepository
    {
        protected override Func<ApplicationToggleValue, bool> GetIdentityEqualityPredicate(
            ApplicationToggleValue businessEntity)
        {
            return t => t.Id == businessEntity.Id;
        }
    }
}
