using Humanizer;
using MediatR;
using MeetingApp.Application.Features.CQRS.Commands;
using MeetingApp.Application.Features.CQRS.Queries;
using MeetingApp.Application.Interfaces;
using MeetingApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using System.Security.Claims;
using System.Web;

namespace MeetingApp.UI.Controllers
{
    [Authorize]
    public class ChangePasswordController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;

        public ChangePasswordController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }

        [HttpGet]
        public IActionResult Auth(string returnUrl)
        {
            ViewBag.pageTitle = "Şifreni Değiştir - Meeting App";
            ViewBag.returnUrl = returnUrl;
            return View(new ChangePasswordViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Auth(ChangePasswordViewModel model)
        {
            var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _mediator.Send(new GetUserQueryRequest(userId));
            if (ModelState.IsValid)
            {
                if (user.Password == model.CurrentPassword)
                {
                    var updatedEntity = new UpdateUserCommandRequest
                    {
                        Id = userId,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        ConfirmCode = user.ConfirmCode,
                        ConfirmEmail = user.ConfirmEmail,
                        Password = model.NewPassword,
                    };
                    return RedirectToAction("Successfull", new { returnUrl = model.returnUrl });
                }
                else
                {
                    ModelState.AddModelError("", "Mevcut şifreniz doğru değil.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        public async Task<IActionResult> Successfull(string returnUrl)
        {
            ViewBag.pageTitle = "Başarılı - Meeting App";
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Index", "Profile");
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        public async Task<IActionResult> SuccessfullForgotPassword(string returnUrl, string email)
        {
            ViewBag.pageTitle = "Başarılı - Meeting App";
            var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _mediator.Send(new GetUserQueryRequest(userId));

            string body = "Kullanıcı Giriş Bilgileriniz:" +
                "<p> Email: " + user.Email + "</p>\n" +
                "<p> Şifre: " + user.Password + "</p>\n" +
                "<p> Güvenliğiniz için kullanıcı bilgilerinizi kimseyle paylaşmayınız. </p>\n";
            await _mailService.SendMessageAsync(email, "Meeting App Kullanıcı Bilgileriniz", body, true);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Index", "Profile");
            }
            ViewBag.returnUrl = returnUrl;
            ViewBag.email = email;
            return View();
        }
    }
}
