using System.ComponentModel.DataAnnotations;

namespace MeetingApp.UI.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Mevcut şifre alanı zorunludur.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifre alanı zorunludur.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifre tekrar alanı zorunludur.")]
        [Compare("NewPassword",ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ReNewPassword { get; set; }
        public string? returnUrl { get; set; }
    }
}
