using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Models
{
    public class FilterModel
    {
        public FilterModel()
        {
            Statuses = new List<string>();
            Classifiers = new List<string>();
            Companies = new List<string>();
            Addresses = new List<string>();
            Periods = new List<Period>();
        }

        [JsonPropertyName("Statuses")]
        public IEnumerable<string> Statuses { get; set; }

        [JsonPropertyName("Classifiers")]
        public IEnumerable<string> Classifiers { get; set; }

        [JsonPropertyName("Companies")]
        public IEnumerable<string> Companies { get; set; }

        [JsonPropertyName("Addresses")]
        public IEnumerable<string> Addresses { get; set; }

        [JsonPropertyName("Periods")]
        public IEnumerable<Period> Periods { get; set; }


        public class Period
        {
            public bool CheckDate(DateTime time) =>
                time >= From && time <= To;

            [JsonPropertyName("From")]
            public DateTime From { get; set; }

            [JsonPropertyName("To")]
            public DateTime To { get; set; }
        }
    }
}
