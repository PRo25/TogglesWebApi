using System.Collections.Generic;
using Toggles.BusinessEntities;

namespace Toggles.Repositories.Contracts
{
    public interface ITogglesRepository : IRepository<Toggle>, ITogglesReadRepository
    {
    }
}
