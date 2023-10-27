using MediatR;
using MeetingApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Features.CQRS.Queries
{
    public class CheckUserQueryRequest : IRequest<CheckUserResponseDto>
    {
        [Required(ErrorMessage = "Email zorunludur.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string? Password { get; set; }
    }
}
