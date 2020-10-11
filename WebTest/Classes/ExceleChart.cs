using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Classes
{
    public class ExceleChart<T, E> : Plotter<T, E>
    {
        
        public ExceleChart()
        {
            //workbook = new Workbook();
        }
        public ExceleChart(IPlottable<T, E> image) : this()
        {
            
        }
        public override void Plot()
        {
            throw new NotImplementedException();
        }
        public override void Plot(IPlottable<T, E> image)
        {
            throw new NotImplementedException();
        }
    }
}