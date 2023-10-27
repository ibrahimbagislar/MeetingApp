using AutoMapper;
using MeetingApp.Application.Dtos;
using MeetingApp.Application.Features.CQRS.Commands;
using MeetingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Mappings
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            this.CreateMap<AppUser, UserListDto>().ReverseMap();
            this.CreateMap<AppUser, UpdateUserCommandRequest>().ReverseMap();
            this.CreateMap<AppUser, ParticipantsListDto>().ReverseMap();
        }
    }
}
