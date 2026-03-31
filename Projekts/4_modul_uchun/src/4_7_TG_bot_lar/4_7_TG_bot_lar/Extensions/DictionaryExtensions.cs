using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_7_TG_bot_lar.Extensions
{
    public static class DictionaryExtensions
    {
        public static IDictionary<string, string> DataRamadan ()
        {
            var _data_ramadan = new Dictionary<string, string>();

            var m = 60;
            var m2 = 19;
            _data_ramadan.Add("19 fevral", "06:01 => 18:18");
            _data_ramadan.Add("20 fevral", "06:00 => 18:19\n\nSaharlik duosi:\nNavaytu an asuma  \r\nsovma shahri romazona  \r\nminal fajri ilal mag'ribi,  \r\nxolisan lillahi ta'ala. Allohu  \r\nakbar." +
                "\n\nIftorlik duosi:\nAllohumma laka sumtu  \r\nva bika aamantu va  \r\naʼlayka tavakkaltu va  \r\naʼlaa rizqika aftartu,  \r\nfagʻfirliy ma qoddamtu va  \r\nmaa axxortu  \r\nbirohmatika yaa arhamar  \r\nroohimiyn.");
           
            for (int i = 21; i < 29; i++)
            {
                _data_ramadan.TryAdd($"{i} fevral", $"05:{--m} => 18:{++m2}\n\nSaharlik duosi:\nNavaytu an asuma  \r\nsovma shahri romazona  \r\nminal fajri ilal mag'ribi,  \r\nxolisan lillahi ta'ala. Allohu  \r\nakbar." +
            "\n\nIftorlik duosi:\nAllohumma laka sumtu  \r\nva bika aamantu va  \r\naʼlayka tavakkaltu va  \r\naʼlaa rizqika aftartu,  \r\nfagʻfirliy ma qoddamtu va  \r\nmaa axxortu  \r\nbirohmatika yaa arhamar  \r\nroohimiyn.");

            }
            for (int i = 1; i < 21; i++)
            {
                _data_ramadan.TryAdd($"{i} mart", $"05:{--m} => 18:{++m2}\n\nSaharlik duosi:\nNavaytu an asuma  \r\nsovma shahri romazona  \r\nminal fajri ilal mag'ribi,  \r\nxolisan lillahi ta'ala. Allohu  \r\nakbar." +
            "\n\nIftorlik duosi:\nAllohumma laka sumtu  \r\nva bika aamantu va  \r\naʼlayka tavakkaltu va  \r\naʼlaa rizqika aftartu,  \r\nfagʻfirliy ma qoddamtu va  \r\nmaa axxortu  \r\nbirohmatika yaa arhamar  \r\nroohimiyn.");

            }
            return _data_ramadan;
        }
    }
}
