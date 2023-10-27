using MediatR;
using MeetingApp.Application.Features.CQRS.Queries;
using MeetingApp.Application.Interfaces;
using MeetingApp.Domain.Entities;
using System.Security.Claims;

namespace MeetingApp.UI.Tools
{
    public class CheckConfirmMail
    {
        private readonly IMediator _mediator;

        public CheckConfirmMail(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Check(string id)
        {
            var user = await _mediator.Send(new GetUserQueryRequest(id));
            if (user.ConfirmEmail)
                return true;
            return false;
        }
    }
}
