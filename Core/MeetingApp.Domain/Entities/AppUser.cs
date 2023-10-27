using MeetingApp.Domain.Entities.Common;

namespace MeetingApp.Domain.Entities
{
    public class AppUser: BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public bool ConfirmEmail { get; set; } = false;
        public int ConfirmCode { get; set; }
        public string? Password { get; set; }

    }
}
