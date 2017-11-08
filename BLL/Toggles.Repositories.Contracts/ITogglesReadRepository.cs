using System;
using System.Collections.Generic;
using Toggles.BusinessEntities;

namespace Toggles.Repositories.Contracts
{
    public interface ITogglesReadRepository
    {
        IList<Toggle> GetAll();
        Toggle GetById(Guid id);
        bool HasAnyById(Guid id);
        bool HasAnyByCodeName(string codeName);
    }
}
