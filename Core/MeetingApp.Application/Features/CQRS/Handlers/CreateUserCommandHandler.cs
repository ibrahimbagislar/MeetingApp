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
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest>
    {
        private readonly IRepository<AppUser> _userRepo;

        public CreateUserCommandHandler(IRepository<AppUser> userRepo)
        {
            _userRepo = userRepo;
        }


        public async Task<Unit> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                Surname = request.Surname,
                ConfirmCode = 0,
            };
            _userRepo.Create(user, JsonPath.AppUser);
            return Unit.Value;
        }
    }
}
