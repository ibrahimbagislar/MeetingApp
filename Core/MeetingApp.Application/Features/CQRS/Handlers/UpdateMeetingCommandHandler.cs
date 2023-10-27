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
    public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommandRequest, UpdateMeetingDto>
    {
        private readonly IRepository<Meeting> _meetingRepo;
        private readonly IMapper _mapper;

        public UpdateMeetingCommandHandler(IRepository<Meeting> meetingRepo, IMapper mapper)
        {
            _meetingRepo = meetingRepo;
            _mapper = mapper;
        }

        public async Task<UpdateMeetingDto> Handle(UpdateMeetingCommandRequest request, CancellationToken cancellationToken)
        {
            var updatedEntity = _mapper.Map<Meeting>(request);
            if (request.PossibleDates.Count == 1)
            {
                updatedEntity.MeetingDate = request.PossibleDates.FirstOrDefault();
            }
            else if(request.PossibleDates.Count >= 1){
                updatedEntity.MeetingDate = null;
            }
            _meetingRepo.Update(updatedEntity,JsonPath.Meetings);
            return _mapper.Map<UpdateMeetingDto>(updatedEntity);
        }
    }
}
