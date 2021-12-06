using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace MacOverflow.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(256, ErrorMessage = "Username is too long enter something less than 256")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Re-enter Password")]
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}