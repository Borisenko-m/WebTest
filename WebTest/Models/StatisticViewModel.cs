using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Models
{
    public class StatisticViewModel 
    {
        public IEnumerable<string> Layers { get; set; } = new List<string>() { "" };
        public IEnumerable<string> ResearchType { get; set; } = new List<string>() { "" };
        public IEnumerable<string> ResearchCriterion { get; set; } = new List<string>() { "" };
        public IEnumerable<string> Companies { get; set; } = new List<string>() { "" };
        public IEnumerable<string> Addresses { get; set; } = new List<string>() { "" };
        public IEnumerable<string> Classifiers { get; set; } = new List<string>() { "" };
        public IEnumerable<DateTime> Periods { get; set; } = new List<DateTime>() { DateTime.MinValue, DateTime.MaxValue };

        public JsonObject<StatisticViewModel> GetJson() => new JsonObject<StatisticViewModel>(this);

        void AddLayer()
        {
            throw new NotImplementedException();
        }

    }
}
