using System;
using System.Collections.Generic;

namespace Toggles.DataAccess.DbEntities
{
    public class ToggleDbEntity
    {
        public Guid Id { get; set; }

        public string CodeName { get; set; }

        public string Description { get; set; }

        public ICollection<ToggleValueDbEntity> Values { get; set; }

        public ToggleDbEntity()
        {
            this.Id = Guid.NewGuid();
            this.Values = new HashSet<ToggleValueDbEntity>();
        }
    }
}
