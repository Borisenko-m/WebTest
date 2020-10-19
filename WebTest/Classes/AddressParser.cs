using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Classes
{
    public class AddressParser
    {
        public static IEnumerable<string> Parse(string address) => address.Split(",");
    }
}
