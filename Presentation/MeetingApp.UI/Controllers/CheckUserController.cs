using MediatR;
using MeetingApp.Application.Features.CQRS.Commands;
using MeetingApp.Application.Features.CQRS.Queries;
using MeetingApp.Application.Interfaces;
using MeetingApp.UI.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingApp.UI.Controllers
{
    public class CheckUserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;

        public CheckUserController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _mediator.Send(new GetUserQueryRequest(userId));
            bool check = new CheckConfirmMail(_mediator).Check(userId).Result;
            if (!check)
            {
                Random rnd = new Random();
                var confirmCode = rnd.Next(100000, 1000000);
                var updatedEntity = new UpdateUserCommandRequest
                {
                    Id = userId,
                    ConfirmEmail = user.ConfirmEmail,
                    Email = user.Email,
                    Name = user.Name,
                    Password = user.Password,
                    Surname = user.Surname,
                    ConfirmCode = confirmCode
                };
                await _mediator.Send(updatedEntity);
                string body = "Meeting App Doğrulama kodunuz : " + updatedEntity.ConfirmCode + " ";
                await _mailService.SendMessageAsync(user.Email, "Meeting App Doğrulama Kodu", body, true);
                TempData["Email"] = user.Email;
                return RedirectToAction("Index", "ConfirmMail");
            }
            else
            {
                return RedirectToAction("AllMeetings", "Meetings");
            }
        }
    }
}
