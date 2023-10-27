using AutoMapper;
using MeetingApp.Application.Dtos;
using MeetingApp.Application.Features.CQRS.Commands;
using MeetingApp.Domain.Entities;

namespace MeetingApp.Application.Mappings
{
    public class MeetingProfile : Profile
    {
        public MeetingProfile()
        {
            this.CreateMap<Meeting, MeetingListDto>().ReverseMap();
            this.CreateMap<Meeting, CreateMeetingDto>().ReverseMap();
            this.CreateMap<Meeting, UpdateMeetingDto>().ReverseMap();
            this.CreateMap<Meeting, UpdateMeetingCommandRequest>().ReverseMap();
        }
    }
}
