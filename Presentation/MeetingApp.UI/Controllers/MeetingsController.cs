using MediatR;
using MeetingApp.Application.Dtos;
using MeetingApp.Application.Features.CQRS.Commands;
using MeetingApp.Application.Features.CQRS.Queries;
using MeetingApp.Domain.Entities;
using MeetingApp.UI.Models;
using MeetingApp.UI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data;
using System.Security.Claims;

namespace MeetingApp.UI.Controllers
{
    [Authorize]
    public class MeetingsController : Controller
    {
        private readonly IMediator _mediator;

        public MeetingsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<bool> CheckUser()
        {
            var userId = UserId();
            var user = await _mediator.Send(new GetUserQueryRequest(userId));
            return new CheckConfirmMail(_mediator).Check(userId).Result;
        }
        public string UserId()
        {
            return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
        public async Task<IActionResult> AllMeetings()
        {
            ViewBag.pageTitle = "Katılabileceğim Toplantılar - Meeting App";
            // kontrol
            bool check = CheckUser().Result;
            if (!check)
            {
                return RedirectToAction("Index", "CheckUser");
            }


            var userId = UserId();

            // Giriş yapan kullanıcının kendi oluşturduğu toplantılar dışındaki toplantıların listesi
            var otherMeetings = new List<MeetingListDto>();
            var datas = await _mediator.Send(new GetAllMeetingQueryRequest());
            foreach (var item in datas)
            {
                if (item.AppUserId != userId && !(item.Participants.Contains(new Participant { Id = userId })))
                    otherMeetings.Add(item);
            }
            return View(otherMeetings);
        }


        public async Task<IActionResult> MyMeetings()
        {
            // kontrol
            bool check = CheckUser().Result;
            if (!check)
            {
                return RedirectToAction("Index", "CheckUser");
            }
            var userId = UserId();

            var myMeetings = new List<MeetingListDto>();
            var datas = await _mediator.Send(new GetAllMeetingQueryRequest());

            foreach (var item in datas)
            {
                if (item.AppUserId == userId)
                {
                    myMeetings.Add(item);
                    ViewBag.pageTitle = "Toplantılarım -" + item.Organizer +"- Meeting App";
                }
            }
            return View(myMeetings);
        }

        public async Task<IActionResult> AttendedMeetings()
        {
            ViewBag.pageTitle = "Katıldığım Toplantılar - Meeting App";
            // kontrol
            bool check = CheckUser().Result;
            if (!check)
            {
                return RedirectToAction("Index", "CheckUser");
            }

            var userId = UserId();


            var attendMeetings = new List<MeetingListDto>();
            var datas = await _mediator.Send(new GetAllMeetingQueryRequest());
            foreach (var item in datas)
            {
                if (item.AppUserId != userId && item.Participants.Any(p => p.Id == userId))
                {
                    attendMeetings.Add(item);
                }
            }
            return View(attendMeetings);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var meetings = await _mediator.Send(new GetMeetingQueryRequest(id));
            ViewBag.pageTitle = meetings.Title + " Toplantısı İçin Detay - Meeting App";

            var listDto = new MeetingDetailListModel
            {
                Description = meetings.Description,
                SelectedDate = null,
                Participants = meetings.Participants,
                AppUserId = meetings.AppUserId,
                CreatedDate = meetings.CreatedDate,
                Id = id,
                MeetingDate = meetings.MeetingDate,
                Organizer = meetings.Organizer,
                PossibleDates = meetings.PossibleDates,
                Title = meetings.Title,
            };
            return View(listDto);
        }
        [HttpPost]
        public async Task<IActionResult> Details(MeetingDetailListModel dto)
        {
            // kontrol
            bool check = CheckUser().Result;
            if (!check)
            {
                return RedirectToAction("Index", "CheckUser");
            }

            var userId = UserId();
            var meeting = await _mediator.Send(new GetMeetingQueryRequest(dto.Id));

            // dto'ya sadece gerekli bilgileri tekrardan verme
            dto.PossibleDates = meeting.PossibleDates;
            dto.Organizer = meeting.Organizer;
            dto.Title = meeting.Title;
            dto.Description = meeting.Description;

            if (ModelState.IsValid && dto.MeetingCode == meeting.MeetingCode)
            {
                // kayıt yaptırılmaya çalışılan toplantı ilgili kullanıcnın kendi toplantısı değil ise kaydı gerçekleştir
                if (!(meeting.AppUserId == userId))
                {
                    var participants = meeting.Participants?.ToList();

                    var newParticipant = new Participant
                    {
                        Id = userId,
                        SelectedDate = dto.SelectedDate,
                    };
                    participants?.Add(newParticipant);

                    var updatedEntity = new UpdateMeetingCommandRequest
                    {
                        AppUserId = meeting.AppUserId,
                        CreatedDate = meeting.CreatedDate,
                        Id = meeting.Id,
                        Description = meeting.Description,
                        Organizer = meeting.Organizer,
                        Participants = participants,
                        PossibleDates = meeting.PossibleDates,
                        Title = meeting.Title,
                        MeetingCode = meeting.MeetingCode,
                    };

                    await _mediator.Send(updatedEntity);
                    return RedirectToAction("AttendedMeetings");
                }
                else
                {
                    ModelState.AddModelError("", "Kayıt yapmaya çalıştığınız toplantı zaten sizin");
                    return View(dto);
                }
            }
            else if (ModelState.IsValid && !(dto.MeetingCode == meeting.MeetingCode))
            {
                ModelState.AddModelError("", "Girdiğiniz toplantı kodu yanlış.");
                return View(dto);
            }
            return View(dto);
        }
        public async Task<IActionResult> Participants(string id)
        {
            // kontrol
            bool check = CheckUser().Result;
            if (!check)
            {
                return RedirectToAction("Index", "CheckUser");
            }

            var userId = UserId();
            // katılımcı listesine bakılmak istenen toplantı o an giriş yapan kullanıcıya mı ait kontrolünü yapıyoruz
            var entity = await _mediator.Send(new GetMeetingQueryRequest(id));
            ViewBag.pageTitle = entity.Title + " Toplantısı Katılımcıları - Meeting App";
            ViewBag.MeetingTitle = entity.Title.Trim();

            var participantList = await _mediator.Send(new GetMeetingParticipantsQueryRequest(id));
            return View(participantList);
        }

        public async Task<IActionResult> Remove(string id)
        {
            // kontrol
            bool check = CheckUser().Result;
            if (!check)
            {
                return RedirectToAction("Index", "CheckUser");
            }

            // silinmek için gönderilen toplantının sahibi giriş yapan kullanıcı ile aynı ise ilgili toplantıyı sil
            var removedEntity = await _mediator.Send(new GetMeetingQueryRequest(id));
            if (removedEntity.AppUserId == UserId())
            {
                await _mediator.Send(new RemoveMeetingCommandRequest(id));
            }
            return RedirectToAction("MyMeetings");
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            // kontrol
            bool check = CheckUser().Result;
            if (!check)
            {
                return RedirectToAction("Index", "CheckUser");
            }
            var updatedEntity = await _mediator.Send(new GetMeetingQueryRequest(id));
            ViewBag.pageTitle = updatedEntity.Title + "- Güncelleme - Meeting App";
            if (updatedEntity.AppUserId == UserId())
            {
                var meeting = await _mediator.Send(new GetMeetingQueryRequest(id));
                return View(meeting);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(MeetingListDto dto)
        {
            // kontrol
            bool check = CheckUser().Result;
            if (!check)
            {
                return RedirectToAction("Index", "CheckUser");
            }

            var currentData = await _mediator.Send(new GetMeetingQueryRequest(dto.Id));
            var request = new UpdateMeetingCommandRequest
            {
                AppUserId = currentData.AppUserId,
                PossibleDates = dto.PossibleDates,
                Description = dto.Description,
                Id = currentData.Id,
                Organizer = currentData.Organizer,
                Title = dto.Title,
                CreatedDate = currentData.CreatedDate,
                Participants = currentData.Participants,
                MeetingDate = currentData.MeetingDate,
            };
            await _mediator.Send(request);
            return RedirectToAction("MyMeetings");
        }

        public async Task<IActionResult> UnRegister(string id)
        {
            // kontrol
            bool check = CheckUser().Result;
            if (!check)
            {
                return RedirectToAction("Index", "CheckUser");
            }

            var userId = UserId();
            var meeting = await _mediator.Send(new GetMeetingQueryRequest(id));
            if (meeting.Participants.Any(x => x.Id == userId))
            {
                // silinecek katılımcı kaydını Id değer üzerinden buluyoruz
                var participantToRemove = meeting.Participants.FirstOrDefault(x => x.Id == userId);

                var participants = meeting.Participants.ToList();
                participants.Remove(participantToRemove);
                // ilgili kaydı katılımcılar arasından sildikten sonra meetingin katılımcılar kısmını güncellyioruz
                var updatedEntity = new UpdateMeetingCommandRequest
                {
                    AppUserId = meeting.AppUserId,
                    CreatedDate = meeting.CreatedDate,
                    Id = meeting.Id,
                    Description = meeting.Description,
                    Organizer = meeting.Organizer,
                    Participants = participants,
                    PossibleDates = meeting.PossibleDates,
                    Title = meeting.Title,
                    MeetingCode = meeting.MeetingCode
                };
                await _mediator.Send(updatedEntity);
            }
            return RedirectToAction("AllMeetings", "Meetings");
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.pageTitle = "Toplantı Oluştur - Meeting App";
            // kontrol
            bool check = CheckUser().Result;
            if (!check)
            {
                return RedirectToAction("Index", "CheckUser");
            }

            return View(new CreateMeetingCommandRequest());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMeetingCommandRequest request)
        {
            // kontrol
            bool check = CheckUser().Result;
            if (!check)
            {
                return RedirectToAction("Index", "CheckUser");
            }

            var userId = UserId();
            var user = await _mediator.Send(new GetUserQueryRequest(userId));
            string organizer = user.Name + " " + user.Surname;
            if (ModelState.IsValid)
            {
                var createdMeeting = new CreateMeetingCommandRequest
                {
                    AppUserId = userId,
                    Description = request.Description,
                    PossibleDates = request.PossibleDates,
                    Organizer = organizer,
                    Title = request.Title,
                };
                await _mediator.Send(createdMeeting);
                return RedirectToAction("MyMeetings");
            }
            else
            {
                return View(request);
            }
        }
    }
}
