using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebTest.Models
{
    public class ReportConfiguration
    {

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("specification")]
        public IEnumerable<string> Specifications { get; set; }

        [JsonPropertyName("from")]
        public DateTime From { get; set; }

        [JsonPropertyName("to")]
        public DateTime To { get; set; }

        public void SpecificationsToLower()
        {
            if(Specifications == null) Specifications = new List<string>();
            Specifications = Specifications.Select(l => l.ToLower());
        }
    }

}
