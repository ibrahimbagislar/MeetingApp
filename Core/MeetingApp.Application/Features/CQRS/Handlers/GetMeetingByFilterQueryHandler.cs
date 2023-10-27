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
    public class GetMeetingByFilterQueryHandler : IRequestHandler<GetMeetingByFilterQueryRequest, MeetingListDto>
    {
        private readonly IRepository<Meeting> _meetingRepo;
        private readonly IMapper _mapper;

        public GetMeetingByFilterQueryHandler(IRepository<Meeting> meetingRepo, IMapper mapper)
        {
            _meetingRepo = meetingRepo;
            _mapper = mapper;
        }

        public async Task<MeetingListDto> Handle(GetMeetingByFilterQueryRequest request, CancellationToken cancellationToken)
        {
            var meetings = _meetingRepo.GetByFilter(request.Filter, JsonPath.Meetings);
            return _mapper.Map<MeetingListDto>(meetings);
        }
    }
}
