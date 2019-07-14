using System.ComponentModel.DataAnnotations;

namespace VideoRental.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
