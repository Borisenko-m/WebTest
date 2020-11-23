using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Models
{
    public class PeriodModel
    {
        public bool CheckDate(DateTime time) =>
            time >= From && time <= To;

        [JsonPropertyName("From")]
        public DateTime From { get; set; }

        [JsonPropertyName("To")]
        public DateTime To { get; set; }

        public override string ToString()
        {
            return $"{From.ToString("yyyy'-'MM'-'dd'")} {To.ToString("yyyy'-'MM'-'dd'")}";
        }
    }
}
