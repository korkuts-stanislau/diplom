using System.ComponentModel.DataAnnotations;

namespace MagikAPI.Models.HelperModels.Profile
{
    public class ProfileName
    {
        [Required]
        [MaxLength(64)]
        public string NewProfileName { get; set; }
    }
}
