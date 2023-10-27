using MediatR;
using MeetingApp.Application.Dtos;
using MeetingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Features.CQRS.Queries
{
    public class GetMeetingByFilterQueryRequest : IRequest<MeetingListDto>
    {
        public Expression<Func<Meeting,bool>> Filter { get; set; }
        public GetMeetingByFilterQueryRequest(Expression<Func<Meeting, bool>> filter)
        {
            Filter = filter;
        }
    }
}
