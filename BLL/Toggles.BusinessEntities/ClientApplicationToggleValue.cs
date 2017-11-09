using System.ComponentModel.DataAnnotations;

namespace Toggles.BusinessEntities
{
    public class ClientApplicationToggleValue : ToggleValue
    {
        [Required]
        public ClientApplication Application { get; set; }
    }
}
