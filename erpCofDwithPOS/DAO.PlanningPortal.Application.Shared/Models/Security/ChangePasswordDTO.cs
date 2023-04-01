using System.ComponentModel.DataAnnotations;

namespace zero.Shared.Models.Security
{
    public class ChangePasswordDTO
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}