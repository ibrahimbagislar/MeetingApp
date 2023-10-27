using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Dtos
{
    public class ParticipantsListDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public DateTime? SelectedDate { get; set; }
    }
}
