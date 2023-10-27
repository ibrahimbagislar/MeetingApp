using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Dtos
{
    public class UserListDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string Surname { get; set; }
        public string? Email { get; set; }
        public bool ConfirmEmail { get; set; }
        public int ConfirmCode { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        public string? Password { get; set; }
    }
}
