using System.ComponentModel.DataAnnotations;

namespace MeetingApp.UI.Models
{
    public class ConfirmMailModel
    {
        [Required(ErrorMessage = "Onay kodunu giriniz.")]
        public int ConfirmCode { get; set; }
        public string Email { get; set; }
    }
}
