using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebTest.Models
{
    public class ChartModel
    {
        public ChartModel()
        {
            Option = new Options();
            Series = new List<Serie>();
        }
        [JsonPropertyName("series")]
        public IEnumerable<Serie> Series { get; set; } = default;

        [JsonPropertyName("options")]
        public Options Option { get; set; } = default;

        public class Options
        {
            public Options()
            {
                XAxis = new XAxis();
            }
            [JsonPropertyName("xaxis")]
            public XAxis XAxis { get; set; } = default;
        }
        public class XAxis
        {
            public XAxis()
            {
                Categories = new List<string>();
            }
            [JsonPropertyName("categories")]
            public IEnumerable<string> Categories { get; set; } = default;
        }
        public class Serie
        {
            public Serie()
            {
                Name = "";
            }
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("data")]
            public IEnumerable<int> Data { get; set; }
        }
    }
}
