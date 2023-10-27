using AutoMapper;
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
    public class GetUserQueryHandler : IRequestHandler<GetUserQueryRequest, UserListDto>
    {
        private readonly IRepository<AppUser> _appUserRepo;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IRepository<AppUser> appUserRepo, IMapper mapper)
        {
            _appUserRepo = appUserRepo;
            _mapper = mapper;
        }

        public async Task<UserListDto> Handle(GetUserQueryRequest request, CancellationToken cancellationToken)
        {
            var user = _appUserRepo.GetById(request.Id,JsonPath.AppUser);
            return _mapper.Map<UserListDto>(user);
        }
    }
}
