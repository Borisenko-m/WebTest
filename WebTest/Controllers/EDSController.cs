using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using WebTest.Models;
using WebTest.Classes.ExcelIO;
namespace WebTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EDSController : ControllerBase
    {
        DBSets DBSets = new DBSets();
        IEnumerable<ApplicationModel> Applications { get; set; }
        FilterModel Filter { get; set; }
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
                    foreach (string t in specifications[category.ToLower()])
                    {
                        yield return t;
                    }
                    break;
                default:
                    break;
            }

        }

        //                      апишка для поиска по разным фильтрам
        //https://localhost:5001/api/EDSChart/specifications/?category=компании&spec=    получаем список всех компании
        //https://localhost:5001/api/EDSChart/specifications/?category=компании&spec=а   получаем список всех компании начинающих на "а"
        //https://localhost:5001/api/EDSChart/specifications/?category=адреса&spec=      получаем список всех регионов начинающих на "а"
        //https://localhost:5001/api/EDSChart/specifications/?category=адреса&spec=а-    получаем список всех районов, которые принадлежать региону "а"
        //https://localhost:5001/api/EDSChart/specifications/?category=адреса&spec=а-н   получаем список всех районов начинающих на "н", которые принадлежать региону "а"
        
            
        [HttpGet("specifications")]
        public IEnumerable<string?> Get(string category, string spec)
        {
            if (category == null) category = "";
            if (spec == null) spec = "";
            string[] specifications = spec.Split("-");

            return (category.ToLower()) switch
            {
                "компании" => new Reports(DBSets).GetCompanies(specifications[0]),
                "адреса" => specifications.Length switch
                {
                    1 => new Reports(DBSets).GetRegions(specifications[0]),
                    2 => new Reports(DBSets).GetDistricts(specifications[0], specifications[1]),
                    3 => new Reports(DBSets).GetCities(specifications[0], specifications[1], specifications[2]),
                    4 => new Reports(DBSets).GetStreets(specifications[0], specifications[1], specifications[2], specifications[3]),
                    _ => default,
                },
                "классификаторы" => new Reports(DBSets).GetClassifiers(specifications[0]),
                _ => default,
            };
        }
        
        [HttpPost("GetAppsByFilter")]
        public string GetAppsByFilter([FromBody] object fromBody)
        {
            var value = fromBody.ToString().Replace("\n", "");
            Filter = JsonSerializer.Deserialize<FilterModel>(value);
            Applications = new Reports(DBSets).GetAppsByFilter(Filter).ToList();
            return new ModelToJson<ApplicationModel>() { Models = Applications }.ToString();
        }

        [HttpGet("GetPoints")]
        public string GetPoints(string chart, string a1, string a2, string selection)
        {
            var model = new ChartModel();
            
            model.Option.XAxis.Categories =
                Enumerable.Range(1, Filter.Periods.Count() - 1).Cast<string>();
            Filter.Periods.ToList().ForEach(period => model.Series.ToList()
                .Add(new ChartModel.Serie()
                {
                    Name = period.ToString()
                }));
            ChartModel.Serie serie;
            PeriodModel period;
            DateTime createdAt;
            int indexOfDay;
            foreach (var app in Applications)
            {
                if (a1 == app.Company && a2 == app.Classifier)
                {
                    createdAt = DateTime.Parse(app.CreatedAt);
                    period = Filter.Periods.Where(period => period.CheckDate(createdAt)).First();
                    serie = model.Series.Where(serie => serie.Name == period.ToString()).ToList().First();
                    serie.Data ??= new List<int>((period.To - period.From).Days);
                    indexOfDay = serie.Data.ToList().Count() - (period.To - createdAt).Days;
                    serie.Data.ToList()[indexOfDay] += 1;
                }
            }
            return JsonSerializer.Serialize(model);
        }
        [HttpGet("GetTable")]
        public string GetTable()
        {
            Statistic statistic = new Statistic();


            return new ModelToJson<ApplicationModel>() { Models = Applications }.ToString();
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

            return new ModelToJson<ApplicationModel>()
            {
                Models = new Reports(DBSets).GetReport(reportModel)
            }.ToString();
        }

        // POST: api/EDSChart/download/?
        [HttpPost("{name}.xlsx")]
        public FileResult PostDownload([FromBody] object fromBody)
        {
            
            var value = fromBody.ToString().Replace("\n", "");
            var reportModel =
                (value is null || value == "") ?
                new ReportConfiguration() :
                JsonSerializer.Deserialize<ReportConfiguration>(value);

            var exIO = new ExcelIO<ApplicationModel>("file", value);  
            exIO.Execute(new Reports(DBSets).GetReport(reportModel));

            return new PhysicalFileResult(exIO.Path, "application/xlsx");
        }
        [HttpGet("test")]
        public FileResult GetDownload(string any)
        {
            var exIO = new ExcelIO<ApplicationModel>("file", "");
            exIO.Test();
            return new PhysicalFileResult(exIO.Path, "application/xlsx"); ;
        }
        // POST: api/EDSChart/jsonTest/?
        [HttpPost("jsonTest")]
        public void PostDownload(string value, bool a)
        {
            Console.WriteLine(value);
        }
    }
}