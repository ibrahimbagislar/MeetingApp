using AutoMapper;
using MediatR;
using MeetingApp.Application.Dtos;
using MeetingApp.Application.Features.CQRS.Commands;
using MeetingApp.Application.Interfaces;
using MeetingApp.Application.Static;
using MeetingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Features.CQRS.Handlers
{
    public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommandRequest, CreateMeetingDto>
    {
        private readonly IRepository<Meeting> _meetingRepo;
        private readonly IMapper _mapper;

        public CreateMeetingCommandHandler(IRepository<Meeting> meetingRepo, IMapper mapper)
        {
            _meetingRepo = meetingRepo;
            _mapper = mapper;
        }

        public async Task<CreateMeetingDto> Handle(CreateMeetingCommandRequest request, CancellationToken cancellationToken)
        {
            var meeting = new Meeting();
            if (request.PossibleDates.Count == 1)
            {
                meeting.Title = request.Title;
                meeting.Description = request.Description;
                meeting.PossibleDates = request.PossibleDates;
                meeting.CreatedDate = DateTime.UtcNow;
                meeting.Organizer = request.Organizer;
                meeting.AppUserId = request.AppUserId;
                meeting.Participants = new List<Participant>();
                meeting.MeetingDate = request.PossibleDates[0].Date;
            }
            else if (request.PossibleDates.Count > 1)
            {
                meeting.Title = request.Title;
                meeting.Description = request.Description;
                meeting.PossibleDates = request.PossibleDates;
                meeting.CreatedDate = DateTime.UtcNow;
                meeting.Organizer = request.Organizer;
                meeting.AppUserId = request.AppUserId;
                meeting.Participants = new List<Participant>();
                meeting.MeetingDate = null;
            }

                _meetingRepo.Create(meeting,JsonPath.Meetings);
            return _mapper.Map<CreateMeetingDto>(meeting);
        }
    }
}
