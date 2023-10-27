using MediatR;
using MeetingApp.Application.Dtos;
using MeetingApp.Application.Features.CQRS.Commands;
using MeetingApp.Application.Features.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingApp.UI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _mediator.Send(new GetUserQueryRequest(userId));
            ViewBag.pageTitle = user.Name + " " + user.Surname + " Profil - Meeting App";
            user.Password = null;
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserListDto dto)
        {
            var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _mediator.Send(new GetUserQueryRequest(userId));

            if(user.Password == dto.Password)
            {
                var updatedEntity = new UpdateUserCommandRequest
                {
                    Id = userId,
                    Name = dto.Name,
                    Surname = dto.Surname,
                    Email = user.Email,
                    ConfirmCode = user.ConfirmCode,
                    ConfirmEmail = user.ConfirmEmail,
                    Password = user.Password,
                };
                await _mediator.Send(updatedEntity);
                return View("Index");
            }
            else if (string.IsNullOrWhiteSpace(dto.Password))
            {
                dto.Email = user.Email;
                return View(dto);
            }
            else
            {
                dto.Email = user.Email;
                ModelState.AddModelError("","Girdiğiniz şifre doğru değil.");
                return View(dto);
            }
        }
    }
}
