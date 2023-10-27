using AutoMapper;
using MediatR;
using MeetingApp.Application.Dtos;
using MeetingApp.Application.Features.CQRS.Queries;
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
    public class GetMeetingParticipantsQueryHandler : IRequestHandler<GetMeetingParticipantsQueryRequest, List<ParticipantsListDto>>
    {
        private readonly IRepository<AppUser> _userRepo;
        private readonly IRepository<Meeting> _meetingRepo;
        private readonly IMapper _mapper;

        public GetMeetingParticipantsQueryHandler(IRepository<AppUser> userRepo, IRepository<Meeting> meetingRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _meetingRepo = meetingRepo;
            _mapper = mapper;
        }

        public async Task<List<ParticipantsListDto>> Handle(GetMeetingParticipantsQueryRequest request, CancellationToken cancellationToken)
        {
            var participantsId = new List<Participant>();
            var meeting = _meetingRepo.GetById(request.Id, JsonPath.Meetings);

            foreach (var item in meeting.Participants)
            {
                participantsId.Add(item);
            }

            var participants = new List<ParticipantsListDto>();
            foreach (var item in participantsId)
            {
                var user = _userRepo.GetById(item.Id, JsonPath.AppUser);
                var newParticipant = new ParticipantsListDto
                {
                    SelectedDate = item.SelectedDate,
                    Name = user.Name,
                    Email = user.Email,
                    Surname = user.Surname,
                };
                participants.Add(newParticipant);
            }
            return _mapper.Map<List<ParticipantsListDto>>(participants);
        }
    }
}
