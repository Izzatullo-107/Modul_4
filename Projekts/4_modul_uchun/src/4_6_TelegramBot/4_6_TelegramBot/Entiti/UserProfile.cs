using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_6_TelegramBot.Entiti;

public class UserProfile
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int DailyUsageCount { get; set; }
    public DateTime LastInteractionDate { get; set; }
}
