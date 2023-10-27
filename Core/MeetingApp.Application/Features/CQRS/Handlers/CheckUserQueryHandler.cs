using MediatR;
using MeetingApp.Application.Dtos;
using MeetingApp.Application.Features.CQRS.Queries;
using MeetingApp.Application.Interfaces;
using MeetingApp.Application.Static;
using MeetingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Features.CQRS.Handlers
{
    public class CheckUserQueryHandler : IRequestHandler<CheckUserQueryRequest, CheckUserResponseDto>
    {
        private readonly IRepository<AppUser> _userRepo;

        public CheckUserQueryHandler(IRepository<AppUser> userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<CheckUserResponseDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
        {
            var dto = new CheckUserResponseDto();

            var user = _userRepo.GetByFilter(x => x.Email == request.Email && x.Password == request.Password,JsonPath.AppUser);
            if (user == null)
                dto.IsExist = false;
            else
            {
                dto.Id = user.Id;
                dto.Email = user.Email;
                dto.Name = user.Name;
                dto.Surname = user.Surname;
                dto.IsExist = true;
            }
            return dto;
        }
    }
}
