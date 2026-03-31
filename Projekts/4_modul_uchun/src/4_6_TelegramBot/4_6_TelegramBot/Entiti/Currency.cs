using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_6_TelegramBot.Entiti;

public class Currency
{
    [JsonProperty("ccy")]
    public string Code { get; set; }
    [JsonProperty("ccyNm_UZ")]
    public string Name { get; set; }
    [JsonProperty("rate")]
    public string Rate { get; set; }
}
