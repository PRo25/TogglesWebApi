using System;
using System.Collections.Generic;
using Toggles.BusinessEntities;

namespace Toggles.BusinessRules.Contracts
{
    public interface ITogglesLoader
    {
        IList<Toggle> GetAll();

        Toggle GetById(Guid id);
    }
}
