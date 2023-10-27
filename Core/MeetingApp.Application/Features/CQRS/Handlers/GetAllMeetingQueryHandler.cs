using AutoMapper;
using MediatR;
using MeetingApp.Application.Dtos;
using MeetingApp.Application.Features.CQRS.Queries;
using MeetingApp.Application.Interfaces;
using MeetingApp.Application.Static;
using MeetingApp.Domain.Entities;

namespace MeetingApp.Application.Features.CQRS.Handlers
{
    public class GetAllMeetingQueryHandler : IRequestHandler<GetAllMeetingQueryRequest, List<MeetingListDto>>
    {
        private readonly IRepository<Meeting> _meetingRepo;
        private readonly IMapper _mapper;

        public GetAllMeetingQueryHandler(IRepository<Meeting> meetingRepo, IMapper mapper)
        {
            _meetingRepo = meetingRepo;
            _mapper = mapper;
        }

        public async Task<List<MeetingListDto>> Handle(GetAllMeetingQueryRequest request, CancellationToken cancellationToken)
        {
            var meetings = _meetingRepo.GetAll(JsonPath.Meetings);
            return _mapper.Map<List<MeetingListDto>>(meetings);
        }
    }
}
