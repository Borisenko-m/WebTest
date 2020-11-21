using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using WebTest.Models;

namespace WebTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        DBSets DBSets = new DBSets();


        [HttpPost]
        public string GetCompaniesStatistic([FromBody] object fromBody)
        {
            var model = new StatisticModel();
            var value = fromBody.ToString().Replace("\n", "");

            ReportConfiguration reportModel;
            reportModel = JsonSerializer.Deserialize<ReportConfiguration>(value);
            reportModel.SpecificationsToLower();
            var report = new Reports(DBSets).GetReport(reportModel).ToList();
            report.ForEach(l =>
                {
                    model.Increment(l.Status, l.Company);
                });

            return new ModelToJson<StatisticModel.Point>()
            {
                Models = model.Points
            }.ToString();
        }
        [HttpPost("GetAppsByFilter")]
        public string GetTable([FromBody] object fromBody)
        {
            var model = new StatisticModel();
            var value = fromBody.ToString().Replace("\n", "");
            var filter = JsonSerializer.Deserialize<FilterModel>(value);
            var report = new Reports(DBSets).GetAppsByFilter(filter).ToList();
            return new ModelToJson<ApplicationModel>() { Models = report }.ToString();
        }
    }
}
