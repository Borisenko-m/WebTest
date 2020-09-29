using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Classes
{
    public class HTMLChart<T, E> : Plotter<T, E>
    {
        

        public HTMLChart(IPlottable<T, E> chart) : base(chart)
        {
            
        }

        public override void Plot()
        {
            throw new NotImplementedException();
        }

        public override void Plot(IPlottable<T, E> plottable)
        {
            throw new NotImplementedException();
        }
    }
}
