using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebTest.Models
{
    public class ChartModel
    {
        [JsonPropertyName("series")]
        public IEnumerable<Serie> Series { get; set; } = default;

        [JsonPropertyName("options")]
        public Options Option { get; set; } = default;

        public class Options
        {
            [JsonPropertyName("xaxis")]
            public XAxis XAxis { get; set; } = default;
        }
        public class XAxis
        {
            [JsonPropertyName("categories")]
            public IEnumerable<string> Categories { get; set; } = default;
        }
        public class Serie
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("data")]
            public IEnumerable<int> Data { get; set; }
        }
    }
}
