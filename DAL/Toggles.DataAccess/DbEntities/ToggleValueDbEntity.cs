using System;

namespace Toggles.DataAccess.DbEntities
{
    public class ToggleValueDbEntity
    {
        public const string GLOBAL_APPLICATION_CODE_NAME = "Global";

        public Guid Id { get; set; }

        public Guid ToggleId { get; set; }

        public bool Value { get; set; }

        public string ApplicationCodeName { get; set; }

        public string ApplicationVersion { get; set; }

        public ToggleDbEntity Toggle { get; set; }

        public ToggleValueDbEntity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
