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
    public class GetMeetingQueryHandler : IRequestHandler<GetMeetingQueryRequest, MeetingListDto>
    {
        private readonly IRepository<Meeting> _meetingRepo;
        private readonly IMapper _mapper;

        public GetMeetingQueryHandler(IRepository<Meeting> meetingRepo, IMapper mapper)
        {
            _meetingRepo = meetingRepo;
            _mapper = mapper;
        }

        public async Task<MeetingListDto> Handle(GetMeetingQueryRequest request, CancellationToken cancellationToken)
        {
            var meeting = _meetingRepo.GetById(request.Id,JsonPath.Meetings);
            return _mapper.Map<MeetingListDto>(meeting);
        }
    }
}
