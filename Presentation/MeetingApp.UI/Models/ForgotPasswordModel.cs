using System.ComponentModel.DataAnnotations;

namespace MeetingApp.UI.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Email alanı zorunludur.")]
        public string Email { get; set; }
        public string returnUrl { get; set; }
    }
}
