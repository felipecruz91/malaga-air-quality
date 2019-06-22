using System;
using Newtonsoft.Json;

namespace AirQuality.FunctionApp.Models
{
    public class Properties
    {
        public string co_F_level { get; set; }
        public string pm1_F_level { get; set; }
        public string pm1_level { get; set; }
        public int no2_F { get; set; }
        public int no2 { get; set; }
        public string co_level { get; set; }
        public DateTime last_measure { get; set; }
        public int pm1_F { get; set; }
        public int co { get; set; }
        public int o3_F { get; set; }
        public int count_SMAQ { get; set; }
        public string o3_level { get; set; }
        public int pm25 { get; set; }
        public int pm1 { get; set; }
        public string pm10_F_level { get; set; }

        [JsonProperty("iuca.level_F_global")]
        public string iucalevel_F_global { get; set; }

        public string pm25_F_level { get; set; }
        public int count_F { get; set; }
        public int pm25_F { get; set; }
        public string no2_F_level { get; set; }
        public int count { get; set; }

        [JsonProperty("iuca.level_global")]
        public string iucalevel_global { get; set; }

        public string pm10_level { get; set; }
        public int co_F { get; set; }
        public int pm10 { get; set; }
        public int o3 { get; set; }
        public int pm10_F { get; set; }
        public string o3_F_level { get; set; }
        public string no2_level { get; set; }
        public string pm25_level { get; set; }
        public string no2_M_level { get; set; }
        public string resp_APP_level { get; set; }

        [JsonProperty("iuca.level_APP_global")]
        public string iucalevel_APP_global { get; set; }

        public int? no2_M { get; set; }
        public string pm25_M_level { get; set; }
        public string o3_M_level { get; set; }
        public string co_M_level { get; set; }
        public int? pm1_M { get; set; }
        public int? count_APP { get; set; }
        public string iaq_APP_level { get; set; }
        public string pm10_APP_level { get; set; }
        public int? pm10_M { get; set; }

        [JsonProperty("iuca.level_M_global")]
        public string iucalevel_M_global { get; set; }

        public int? o3_M { get; set; }
        public int? pm25_M { get; set; }
        public int? count_M { get; set; }
        public string pm10_M_level { get; set; }
        public string pm1_M_level { get; set; }
        public int? co_M { get; set; }
        public string co_APP_level { get; set; }
    }
}