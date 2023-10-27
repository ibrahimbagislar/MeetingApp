using MeetingApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Domain.Entities
{
    public class Meeting : BaseEntity
    {
        public string MeetingCode { get; set; } = Guid.NewGuid().ToString();
        public string? Title { get; set; }
        public string? Organizer { get; set; }
        public List<DateTime>? PossibleDates { get; set; }
        public DateTime? MeetingDate { get; set; }
        public DateTime CreatedDate{ get; set; }
        public List<Participant>? Participants { get; set; }
        public string? Description { get; set; }
        public string? AppUserId { get; set; }
    }
}
