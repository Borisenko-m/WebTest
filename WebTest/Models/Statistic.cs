using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Models
{
    public class Statistic
    {
        public Statistic() { }
        public Statistic(IEnumerable<ApplicationModel> applications)
        {

        }

        public ChartModel GetChart(FilterModel filter, IEnumerable<ApplicationModel> applications, string type, string a1, string a2)
        {
            var model = new ChartModel();

            switch (type)
            {
                case 1: break;
                case 2: break;
                case 3: break;
            }

            model.Option.XAxis.Categories =
                Enumerable.Range(1, filter.Periods.Count() - 1)
                .Cast<string>();
            filter.Periods.ToList()
                .ForEach(period =>
                model.Series.ToList()
                .Add(new ChartModel.Serie()
                {
                    Name = period.ToString()
                }));
            ChartModel.Serie serie;
            PeriodModel period;
            DateTime createdAt;
            int indexOfDay;
            foreach (var app in applications)
            {
                if (a1 == app.Company && a2 == app.Classifier)
                {
                    createdAt = DateTime.Parse(app.CreatedAt);
                    period = filter.Periods.Where(period => period.CheckDate(createdAt)).First();
                    serie = model.Series.Where(serie => serie.Name == period.ToString()).ToList().First();
                    serie.Data ??= new List<int>((period.To - period.From).Days);
                    indexOfDay = serie.Data.ToList().Count() - (period.To - createdAt).Days;
                    serie.Data.ToList()[indexOfDay] += 1;
                }
            }
        }

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
}
