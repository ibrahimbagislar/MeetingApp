using MediatR;
using MeetingApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Features.CQRS.Queries
{
    public class GetMeetingParticipantsQueryRequest : IRequest<List<ParticipantsListDto>>
    {
        public string? Id { get; set; }
        public GetMeetingParticipantsQueryRequest(string id)
        {
            Id = id;
        }
    }
}
