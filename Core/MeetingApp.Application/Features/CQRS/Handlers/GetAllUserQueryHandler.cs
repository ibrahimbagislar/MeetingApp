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
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, List<UserListDto>>
    {
        private readonly IRepository<AppUser> _appUserRepo;
        private readonly IMapper _mapper;
        public GetAllUserQueryHandler(IRepository<AppUser> appUserRepo, IMapper mapper)
        {
            _appUserRepo = appUserRepo;
            _mapper = mapper;
        }

        public async Task<List<UserListDto>> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
            var users = _appUserRepo.GetAll(JsonPath.AppUser);
            return _mapper.Map<List<UserListDto>>(users);
        }
    }
}
