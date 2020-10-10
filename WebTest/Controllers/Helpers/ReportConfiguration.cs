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
        //public string Category {
        //    get {
        //        return Category;
        //    }
        //    set {
        //        if (value == "Все") value = "";
        //        Category = value;
        //    }
        //}
        public string[] specifications { get; set; }

        [JsonPropertyName("from")]
        public DateTime From { get; set; }

        [JsonPropertyName("to")]
        public DateTime To { get; set; }

        //public ReportConfiguration()
        //{
        //    Type = "Заявки";
        //    Category = "Принята";
        //    From = DateTime.Parse("2019-12-12");
        //    To = DateTime.Now;
        //}
    }

}
