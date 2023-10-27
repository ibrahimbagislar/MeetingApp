using MediatR;
using MeetingApp.Application.Dtos;

namespace MeetingApp.Application.Features.CQRS.Queries
{
    public class GetAllMeetingQueryRequest : IRequest<List<MeetingListDto>>
    {
    }
}
