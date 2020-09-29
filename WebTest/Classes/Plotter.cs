using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;

namespace WebTest.Classes
{
    public abstract class Plotter<T, E> : IPlotter<T, E>
    {
        public Plotter() { }
        public Plotter(IPlottable<T, E> plottable)
        {
            Plotable = plottable;
        }

        protected IPlottable<T, E> Plotable { get; }

        public abstract void Plot();
        public abstract void Plot(IPlottable<T, E> plottable);

        public IEnumerable<ValueTuple<T, E>> CreatePoints(IEnumerable<T> x, IEnumerable<E> y)
        {
            foreach (var order in Enumerable.Range(0, x.Count()))
                yield return new ValueTuple<T, E>((x as List<T>)[order], (y as List<E>)[order]);
        }
    }
}
