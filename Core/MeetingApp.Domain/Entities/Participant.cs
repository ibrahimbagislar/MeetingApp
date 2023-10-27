using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Domain.Entities
{
    public class Participant
    {
        public string? Id { get; set; }
        public DateTime? SelectedDate { get; set; }
    }
}
