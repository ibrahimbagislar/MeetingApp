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
    public class GetUserByFilterQueryHandler : IRequestHandler<GetUserByFilterQueryRequest, UserListDto>
    {
        private readonly IRepository<AppUser> _userRepo;
        private readonly IMapper _mapper;

        public GetUserByFilterQueryHandler(IRepository<AppUser> userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserListDto> Handle(GetUserByFilterQueryRequest request, CancellationToken cancellationToken)
        {
            var user = _userRepo.GetByFilter(request.Filter,JsonPath.AppUser);
            return _mapper.Map<UserListDto>(user);
        }
    }
}
