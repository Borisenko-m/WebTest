using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Classes
{
    /// <summary>
    /// This interface that give ability to plot a chart
    /// </summary>
    public interface IPlotter<T, E>
    {
        /// <summary>
        /// This method that draw a chart.
        /// </summary>
        /// <param name="plottable">This point is relation of T value and E label</param>
        void Plot(IPlottable<T, E> plottable);
        /// <summary>
        /// This method creates points from any data types
        /// </summary>
        /// <param name="a">First parameter is evaluation criterion</param>
        /// <param name="b">Second parameter is delimiter</param>
        /// <returns></returns>
        IEnumerable<(T, E)> CreatePoints(IEnumerable<T> a, IEnumerable<E> b);
        
    }
}
