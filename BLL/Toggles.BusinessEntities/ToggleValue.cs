using System;
using System.ComponentModel.DataAnnotations;

namespace Toggles.BusinessEntities
{
    public class ToggleValue
    {
        public Guid Id { get; set; }

        [Required]
        public bool Value { get; set; }

        public Guid ToggleId { get; set; }

        public Toggle Toggle { get; set; }

        public ToggleValue()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
