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
        DBSets DBSets = new DBSets();

        private IEnumerable<string> types = new List<string>() {
            "Все", "Принята", "Архив", "В работе", "Выполнено без акта",
            "Выполнено с актом", "Закрыта без подтверждения",
            "Закрыта с подтверждением", "Ожидает подтверждения центром",
            "Отказ (нет договора с УО)", "Требует доработки", "Отложено",
            "Отклонено", "Импортирована с ЕДС", "Контроль ГЖИ: Внеплановая проверка",
            "Закрыто (ГЖИ)", "Отправлено в добродел"
        };

        private IEnumerable<string> categories = new List<string>() { "Компании", "Адреса", "Классификаторы" };
        public Dictionary<string, IEnumerable<string>> specifications { get; set; } = new Dictionary<string, IEnumerable<string>>()
        {
            {"компании", new List<string>() { "Компании" }},
            {"адреса", new List<string>() { "Регион", "Район", "Город", "Улица" }},
            {"классификаторы", new List<string>() { "Классификаторы" }}
        };




        //апишка для поиска типов, категории и получения фильтров
        //https://localhost:5001/api/EDSChart/?type=Все&catrgory=Адреса&method=2 - возвращает фильтры
        //https://localhost:5001/api/EDSChart/?type=Все&catrgory=Адрес&method=1 - возвращает ["Адреса"]
        [HttpGet]
        public IEnumerable<string?> Get(string type, string category, short method)
        {
            switch (method)
            {
                case 0://полученеие типов
                    if (type == null) type = "";
                    foreach (string t in types)
                    {
                        if (t.ToLower().Contains(type.ToLower())) yield return t;
                    }
                    break;
                case 1: //полученеие категории
                    if (category == null) category = "";
                    foreach (string c in categories)
                    {
                        if (c.ToLower().Contains(category.ToLower())) yield return c;
                    }
                    break;
                case 2: //полученеие спецификации (фильтры)
                    if (category == null) category = "";
                    if (!specifications.ContainsKey(category.ToLower())) break;
                    foreach (string t in specifications[category])
                    {
                        yield return t;
                    }
                    break;
                default:
                    break;
            }

        }

        //апишка для поиска по разным фильтрам
        //https://localhost:5001/api/EDSChart/specifications/?type=компании&spec=    получаем список всех компании
        //https://localhost:5001/api/EDSChart/specifications/?type=компании&spec=а   получаем список всех компании начинающих на "а"
        //https://localhost:5001/api/EDSChart/specifications/?type=адреса&spec=а     получаем список всех регионов начинающих на "а"
        //https://localhost:5001/api/EDSChart/specifications/?type=адреса&spec=а-    получаем список всех районов, которые принадлежать региону "а"
        //https://localhost:5001/api/EDSChart/specifications/?type=адреса&spec=а-н   получаем список всех районов начинающих на "н", которые принадлежать региону "а"
        [HttpGet("specifications")]
        public IEnumerable<string?> Get(string type, string spec)
        {
            if (type == null) type = "";
            if (spec == null) spec = "";
            string[] specifications = spec.Split("-");

            switch (type.ToLower())
            {
                case "компании":
                    return new Reports(DBSets).GetCompanies(specifications[0]);
                case "адреса":
                    switch (specifications.Length)
                    {
                        case 1:
                            return new Reports(DBSets).GetRegions(specifications[0]);
                        case 2:
                            return new Reports(DBSets).GetDistricts(specifications[0], specifications[1]);
                        case 3:
                            return new Reports(DBSets).GetCities(specifications[0], specifications[1], specifications[2]);
                        case 4:
                            return new Reports(DBSets).GetStreets(specifications[0], specifications[1], specifications[2], specifications[3]);
                        default:
                            return new List<string>();
                    }
                case "классификаторы":
                    return new Reports(DBSets).GetClassifiers(specifications[0]);
                default:
                    return new List<string>();
            }

        }


        // POST: api/EDSChart
        //получаем из фрона JSON с конфигурации отчета (тип, категория, фильтры (спецификации) и дату)
        [HttpPost]
        public string? Post([FromBody] object fromBody)
        {
            var value = fromBody.ToString().Replace("\n", "");

            ReportConfiguration reportModel;
            reportModel = JsonSerializer.Deserialize<ReportConfiguration>(value);
            reportModel.SpecificationsToLower();
            return new Reports(DBSets).GetReport(reportModel);
        }


        // POST: api/EDSChart/download
        //пока не работает
        [HttpPost("download")]
        public string? PostDownload(string value)
        {
            ReportConfiguration reportModel;
            if (value == null || value.Length == 0) reportModel = new ReportConfiguration();
            else reportModel = JsonSerializer.Deserialize<ReportConfiguration>(value);
            return new Reports(DBSets).GetReport(reportModel);
        }

    }
}
