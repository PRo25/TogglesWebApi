using System.ComponentModel.DataAnnotations;

namespace Toggles.BusinessEntities
{
    public class ApplicationToggleValue : ToggleValue
    {
        [Required]
        public Application Application { get; set; }
    }
}
