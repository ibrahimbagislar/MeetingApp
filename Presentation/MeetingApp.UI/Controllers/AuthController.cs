using MediatR;
using MeetingApp.Application.Features.CQRS.Commands;
using MeetingApp.Application.Features.CQRS.Queries;
using MeetingApp.UI.Tools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;

namespace MeetingApp.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            ViewBag.pageTitle = "Giriş Yap - Meeting App";
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("AllMeetings", "Meetings");
            return View(new CheckUserQueryRequest());
        }
        [HttpPost]
        public async Task<IActionResult> Login(CheckUserQueryRequest request)
        {
            var dto = await _mediator.Send(request);
            if (ModelState.IsValid && dto.IsExist)
            {
                var jwtToken = JwtTokenGenerator.GenerateToken(dto);
                JwtSecurityTokenHandler handler = new();
                var token = handler.ReadJwtToken(jwtToken);
                var claims = token.Claims.ToList();
                if (token != null)
                    claims.Add(new Claim("accessToken", jwtToken));

                var claimsIdentiy = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire),
                };

                await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentiy),authProperties);
                return RedirectToAction("Index", "CheckUser");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                return View(request);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.pageTitle = "Kayıt Ol - Meeting App";
            return View(new CreateUserCommandRequest());
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateUserCommandRequest request)
        {
            if (ModelState.IsValid)
            {

                var users = await _mediator.Send(new GetAllUserQueryRequest());
                var checkUser = users.FirstOrDefault(x => x.Email == request.Email);
                if (checkUser == null)
                {
                    await _mediator.Send(request);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("",checkUser.Email + " zaten kullanılıyor.");
                    return View(request);
                }
            }
            else
            {
                return View(request);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
