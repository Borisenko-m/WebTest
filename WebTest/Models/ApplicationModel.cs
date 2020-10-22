using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Models
{
    public class ApplicationModel
    {
        public string ApplicationID { get; set; }
        public string CreatedAt { get; set; }
        public string Address { get; set; }
        public string Classifier { get; set; }
        public string UserFullName { get; set; }
        public string UserPhone { get; set; }
        public string Company { get; set; }
        public string ApplicationStatus { get; set; }
        public string Description { get; set; }
    }
}
