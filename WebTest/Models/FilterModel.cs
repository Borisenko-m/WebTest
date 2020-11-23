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
            Periods = new List<PeriodModel>();
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
        public IEnumerable<PeriodModel> Periods { get; set; }

    }
}
