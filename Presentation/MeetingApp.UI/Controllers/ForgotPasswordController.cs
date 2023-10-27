using MediatR;
using MeetingApp.Application.Features.CQRS.Queries;
using MeetingApp.Application.Interfaces;
using MeetingApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingApp.UI.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;

        public ForgotPasswordController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            ViewBag.pageTitle = "Şifremi Unuttum - Meeting App";
            return View(new ForgotPasswordModel { returnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Index(ForgotPasswordModel model)
        {
            var users = await _mediator.Send(new GetAllUserQueryRequest());
            if (ModelState.IsValid)
            {
                if(users.Any(x => x.Email == model.Email))
                {
                    var user = await _mediator.Send(new GetUserByFilterQueryRequest(x => x.Email == model.Email));
                    string body = "Kullanıcı Giriş Bilgileriniz:" +
                        "<p> Email: " + user.Email + "</p>\n" +
                        "<p> Şifre: " + user.Password + "</p>\n" +
                        "<p> Güvenliğiniz için kullanıcı bilgilerinizi kimseyle paylaşmayınız. </p>\n";
                    await _mailService.SendMessageAsync(model.Email, "Meeting App Kullanıcı Bilgileriniz", body, true);
                }
                return RedirectToAction("Successfull", new { returnUrl = model.returnUrl , email = model.Email});
            }
            return View(model);
        }
        public async Task<IActionResult> Successfull(string returnUrl, string email)
        {
            ViewBag.pageTitle = "Başarılı - Meeting App";
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.returnUrl = returnUrl;
            ViewBag.email = email;
            return View();
        }
    }
}
