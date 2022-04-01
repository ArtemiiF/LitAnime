using System.ComponentModel.DataAnnotations;

namespace LitAnime.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [UIHint("password")]
        [Display(Name = "Password")]
        public string? Password { get; set; }

    }
}
