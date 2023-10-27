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
    public class GetUserByFilterQueryRequest : IRequest<UserListDto>
    {
        public Expression<Func<AppUser, bool>> Filter { get; set; }
        public GetUserByFilterQueryRequest(Expression<Func<AppUser, bool>> filter)
        {
            Filter = filter;
        }
    }
}
