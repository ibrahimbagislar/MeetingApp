using MeetingApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MeetingApp.UI.Models
{
    public class MeetingDetailListModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Lütfen toplantı sahibi tarafından size iletilen toplantı kodunu giriniz.")]
        public string MeetingCode { get; set; }
        public string? Title { get; set; }
        public string? Organizer { get; set; }
        public List<DateTime>? PossibleDates { get; set; }
        public DateTime? MeetingDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Participant>? Participants { get; set; }
        [Required(ErrorMessage = "Lütfen olası tarihlerden birini seçin.")]
        public DateTime? SelectedDate { get; set; }
        public string? Description { get; set; }
        public string? AppUserId { get; set; }
    }
}
