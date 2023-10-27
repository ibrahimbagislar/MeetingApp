using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Features.CQRS.Commands
{
    public class RemoveMeetingCommandRequest : IRequest
    {
        public string Id { get; set; }
        public RemoveMeetingCommandRequest(string id)
        {
            Id = id;
        }
    }
}
