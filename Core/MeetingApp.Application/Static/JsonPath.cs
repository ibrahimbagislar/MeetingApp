using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Application.Static
{
    public class JsonPath
    {
        public static string Meetings => Path.Combine(Directory.GetCurrentDirectory(), "..","..", "Core", "MeetingApp.Domain", "Json", "Meetings.json");
        public static string AppUser => Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Core", "MeetingApp.Domain", "Json", "AppUser.json");
    }
}
