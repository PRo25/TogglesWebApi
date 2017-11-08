using System;
using System.Collections.Generic;
using System.Text;

namespace Toggles.Repositories.Contracts
{
    public interface IRepository<TBusinessEntity>
        where TBusinessEntity : class
    {
        IList<TBusinessEntity> Added { get; }
        IList<TBusinessEntity> Updated { get; }
        IList<TBusinessEntity> Deleted { get; }

        void Add(TBusinessEntity businessEntity);
        void Update(TBusinessEntity businessEntity);
        void Delete(TBusinessEntity businessEntity);
    }
}
