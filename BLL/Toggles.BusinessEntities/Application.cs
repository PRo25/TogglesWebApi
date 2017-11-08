using System.ComponentModel.DataAnnotations;

namespace Toggles.BusinessEntities
{
    public class Application
    {
        [Required]
        public string CodeName { get; set; }

        public string Version { get; set; }
    }
}
