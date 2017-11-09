using System;
using Toggles.BusinessEntities;
using Toggles.Repositories.Contracts;

namespace Toggles.Repositories
{
    public class ClientApplicationToggleValuesRepository : RepositoryBase<ClientApplicationToggleValue>,
        IClientApplicationToggleValuesRepository
    {
        protected override Func<ClientApplicationToggleValue, bool> GetIdentityEqualityPredicate(
            ClientApplicationToggleValue businessEntity)
        {
            return t => t.Id == businessEntity.Id;
        }
    }
}
