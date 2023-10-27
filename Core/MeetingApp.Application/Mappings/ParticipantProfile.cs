using AutoMapper;
using MeetingApp.Application.Dtos;
using MeetingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Mappings
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            this.CreateMap<Participant, ParticipantsListDto>().ReverseMap();
        }
    }
}
