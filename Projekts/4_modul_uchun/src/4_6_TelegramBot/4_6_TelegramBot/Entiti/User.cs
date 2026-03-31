using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_6_TelegramBot.Entiti;

public class User // UserEntity
{
    public long ChatId { get; set; } // Telegramdagi chat IDsini saqlash uchun
    public string Nik_User { get; set; } // Username (@ bilan boshlanadigan)
    public string Ismi { get; set; }
    public string? Familiyasi { get; set; }
    public string TelefonRaqam { get; set; }
    public DateTime RegistirTime { get; set; }
    public int Count { get; set; } // Kuniga necha marta ishlatsa sana ketadi
}
