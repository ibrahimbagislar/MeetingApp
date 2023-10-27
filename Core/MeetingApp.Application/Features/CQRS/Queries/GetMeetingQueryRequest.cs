using MediatR;
using MeetingApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Features.CQRS.Queries
{
    public class GetMeetingQueryRequest : IRequest<MeetingListDto>
    {
        public string Id { get; set; }
        public GetMeetingQueryRequest(string id)
        {
            Id = id;
        }
    }
}
