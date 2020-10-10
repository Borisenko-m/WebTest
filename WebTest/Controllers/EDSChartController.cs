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
    public class EDSChartController : ControllerBase
    {
        private IEnumerable<string> types = new List<string>() { "Заявки" };
        private IEnumerable<string> categories = new List<string>() {
            "Все", "Принята", "Архив", "В работе", "Выполнено без акта",
            "Выполнено с актом", "Закрыта без подтверждения",
            "Закрыта с подтверждением", "Ожидает подтверждения центром",
            "Отказ (нет договора с УО)", "Требует доработки", "Отложено",
            "Отклонено", "Импортирована с ЕДС", "Контроль ГЖИ: Внеплановая проверка",
            "Закрыто (ГЖИ)", "Отправлено в добродел"
        };


        // GET: api/EDSChart
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "Заявки", "Классификаторы" };
        //}

        // GET: /api/EDSChart/5?from=2020-01-01&to=2020-02-28
        //[HttpGet("{from, to}", Name = "Get")]
        //public string? Get(string from, string to)
        //{
        //    DateTime f;
        //    DateTime t;
        //    try
        //    {
        //        f = DateTime.Parse(from);
        //        t = DateTime.Parse(to);
        //    }
        //    catch (Exception)
        //    {
        //        f = DateTime.Today;
        //        t = DateTime.Today;
        //    }


        //    //https://localhost:5001/api/EDSChart/5?id=2&name=3

        //    return new Reports().GetReport(f, t);
        //}


        //выбор по типам
        //[HttpGet]
        //public IEnumerable<string?> Get(string type)
        //{
        //    if (type == null) type = "";
        //    foreach (string t in types)
        //    {
        //        if (t.ToLower().Contains(type.ToLower())) yield return t;
        //    }

        //    //https://localhost:5001/api/EDSChart/?type=
        //}


        //выбор по категориям
        [HttpGet]
        public IEnumerable<string?> Get(string type, string category, short method)
        {
            switch (method)
            {
                case 0:
                    if (type == null) type = "";
                    foreach (string t in types)
                    {
                        if (t.ToLower().Contains(type.ToLower())) yield return t;
                    }
                    break;
                case 1:
                    if (category == null) category = "";
                    foreach (string c in categories)
                    {
                        if (c.ToLower().Contains(category.ToLower())) yield return c;
                    }
                    break;
                case 2:
                    break;
                default:
                    break;
            }

            //https://localhost:5001/api/EDSChart/?type=
        }


        // POST: api/EDSChart
        [HttpPost]
        public string? Post([FromBody] object fromBody)
        {
            var value = fromBody.ToString().Replace("\n", "");

            ReportConfiguration reportModel;
            //if (value == null || value.Length == 0) reportModel = new ReportConfiguration();
            reportModel = JsonSerializer.Deserialize<ReportConfiguration>(value);
            return new Reports().GetReport(reportModel);
        }

        // POST: api/EDSChart/download
        [HttpPost("download")]
        public string? PostDownload(string value)
        {
            ReportConfiguration reportModel;
            if (value == null || value.Length == 0) reportModel = new ReportConfiguration();
            else reportModel = JsonSerializer.Deserialize<ReportConfiguration>(value);
            return new Reports().GetReport(reportModel);
        }

    }
}
