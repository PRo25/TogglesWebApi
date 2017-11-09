using System.ComponentModel.DataAnnotations;

namespace Toggles.BusinessEntities
{
    public class ClientApplication
    {
        [Required]
        public string CodeName { get; set; }

        public string Version { get; set; }
    }
}
