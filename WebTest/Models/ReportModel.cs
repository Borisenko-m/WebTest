using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Models
{
    public class ReportModel
    {
        public string ApplicationID { get; set; }
        public string CreatedAt { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Classifier { get; set; }
        public string UserF { get; set; }
        public string UserM { get; set; }
        public string UserL { get; set; }
        public string UserP { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
    }
}
