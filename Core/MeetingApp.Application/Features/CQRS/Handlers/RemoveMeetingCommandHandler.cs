
using MediatR;
using MeetingApp.Application.Features.CQRS.Commands;
using MeetingApp.Application.Interfaces;
using MeetingApp.Application.Static;
using MeetingApp.Domain.Entities;

namespace MeetingApp.Application.Features.CQRS.Handlers
{
    public class RemoveMeetingCommandHandler : IRequestHandler<RemoveMeetingCommandRequest>
    {
        private readonly IRepository<Meeting> _meetingRepo;

        public RemoveMeetingCommandHandler(IRepository<Meeting> meetingRepo)
        {
            _meetingRepo = meetingRepo;
        }

        public async Task<Unit> Handle(RemoveMeetingCommandRequest request, CancellationToken cancellationToken)
        {
            _meetingRepo.Remove(request.Id, JsonPath.Meetings);
            return Unit.Value;
        }
    }
}
