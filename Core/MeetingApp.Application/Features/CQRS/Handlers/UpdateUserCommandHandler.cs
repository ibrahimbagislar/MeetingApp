using AutoMapper;
using MediatR;
using MeetingApp.Application.Features.CQRS.Commands;
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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest>
    {
        private readonly IRepository<AppUser> _userRepo;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IRepository<AppUser> userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var updatedEntity = _mapper.Map<AppUser>(request);
            _userRepo.Update(updatedEntity,JsonPath.AppUser);
            return Unit.Value;
        }
    }
}
