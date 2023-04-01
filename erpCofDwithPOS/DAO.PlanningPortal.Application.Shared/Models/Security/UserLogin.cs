using System.ComponentModel.DataAnnotations;

namespace zero.Shared.Models.Security;

public class UserLogin : LogableModel
{
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
}