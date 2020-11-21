using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Models
{
    public class ModelToJson<Model>
    {
        public IEnumerable<Model> Models { get; set; }

        public JsonObject<IEnumerable<Model>> GetJson() => new JsonObject<IEnumerable<Model>>(Models);
        public override string ToString() => new JsonObject<IEnumerable<Model>>(Models).Json;
    }
}
