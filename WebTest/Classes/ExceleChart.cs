using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;

namespace WebTest.Classes
{
    public class ExceleChart<T, E> : Plotter<T, E>
    {
        Workbook workbook;
        public ExceleChart()
        {
            workbook = new Workbook();
            workbook.SaveToHtml("ddd");
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