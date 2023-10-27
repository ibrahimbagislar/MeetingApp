using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Features.CQRS.Commands
{
    public class CreateUserCommandRequest : IRequest
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Şifreler eşleşmiyor.")]
        public string? RePassword { get; set; }
    }
}
