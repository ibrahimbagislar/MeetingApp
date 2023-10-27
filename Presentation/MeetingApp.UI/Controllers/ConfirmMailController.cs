using MediatR;
using MeetingApp.Application.Features.CQRS.Commands;
using MeetingApp.Application.Features.CQRS.Queries;
using MeetingApp.Application.Interfaces;
using MeetingApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.Security.Claims;

namespace MeetingApp.UI.Controllers
{
    [Authorize]
    public class ConfirmMailController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;
        public ConfirmMailController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.pageTitle = "Mail Adresini Doğrula - Meeting App";
            return View(new ConfirmMailModel
            {
                Email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
            });
        }
        [HttpPost]
        public async Task<IActionResult> Index(ConfirmMailModel model)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _mediator.Send(new GetUserQueryRequest(userId));
            if (ModelState.IsValid)
            {
                if (user.ConfirmCode == model.ConfirmCode)
                {
                    var updatedConfirmMail = new UpdateUserCommandRequest
                    {
                        Id = user.Id,
                        ConfirmEmail = true,
                        Email = user.Email,
                        Name = user.Name,
                        Password = user.Password,
                        Surname = user.Surname
                    };
                    await _mediator.Send(updatedConfirmMail);
                    return RedirectToAction("AllMeetings", "Meetings");
                }
                else
                {
                    ModelState.AddModelError("", "Doğrulama kodu yanlış.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}
