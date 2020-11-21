using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Models
{
    public class StatisticModel
    {
        public StatisticModel()
        {
            Points = new List<Point>();
        }
        public IEnumerable<Point> Points { get; }

        public void Increment(string zLabel, string xLabel, bool match = true, bool match1 = true)
        {
            if (match && match1)
            {
                var list = ((List<Point>)Points);
                var point = list.Find(l => l.XLabel == xLabel && l.ZLabel == zLabel);

                if (point is null)
                    list.Add( new Point { ZLabel = zLabel, XLabel = xLabel, Y = 1 });
                else point.Y++;
            }
        }

        public class Point
        {
            public int Y { get; set; }
            public string XLabel { get; set; }
            public string ZLabel { get; set; }
        }
    }
}
