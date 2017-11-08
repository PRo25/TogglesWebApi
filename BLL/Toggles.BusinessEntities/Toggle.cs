using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Toggles.BusinessEntities
{
    public class Toggle
    {
        public Guid Id { get; set; }

        [Required]
        public string CodeName { get; set; }

        public string Description { get; set; }

        public IList<ApplicationToggleValue> Values { get; set; }

        public Toggle()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
