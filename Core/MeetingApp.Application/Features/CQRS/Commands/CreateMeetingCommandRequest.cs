using MediatR;
using MeetingApp.Application.Dtos;
using MeetingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Features.CQRS.Commands
{
    public class CreateMeetingCommandRequest : IRequest<CreateMeetingDto>
    {
        public string? MeetingCode { get; set; }
        public string? Title { get; set; }
        public string? Organizer { get; set; }
        public List<DateTime>? PossibleDates { get; set; }
        public DateTime? MeetingDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Participant>? Participants { get; set; }
        public string? Description { get; set; }
        public string? AppUserId { get; set; }
    }
}
