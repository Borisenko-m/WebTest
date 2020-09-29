using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTest.Models;

namespace WebTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EDSChartController : ControllerBase
    {


        // GET: api/EDSChart
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Заявки", "Классификаторы" };
        }

        // GET: /api/EDSChart/5?from=2020-01-01&to=2020-02-28
        [HttpGet("{from, to}", Name = "Get")]
        public string? Get(string from, string to)
        {
            DateTime f;
            DateTime t;
            try
            {
                f = DateTime.Parse(from);
                t = DateTime.Parse(to);
            }
            catch (Exception)
            {
                f = DateTime.Today;
                t = DateTime.Today;
            }


            //https://localhost:5001/api/EDSChart/5?id=2&name=3

            return new Reports().GetReport(f, t);
        }

        // POST: api/EDSChart
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT: api/EDSChart/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
