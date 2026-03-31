using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _4_6_TelegramBot.DTOs
{
    public class ValutaDto
    {
        [JsonPropertyName("Ccy")]
        public string ValyutaKodi { get; set; }
        [JsonPropertyName("CcyNm_UZ")]
        public string ValyutaNomi { get; set; }
        [JsonPropertyName("Rate")]
        public string Kurs { get; set; }

        [JsonPropertyName("Date")]
        public string Sana { get; set; }
    }
}
