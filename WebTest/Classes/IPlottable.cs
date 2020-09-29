using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Classes
{
    public interface IPlottable<T, E>
    {
        public string Label { get; set; }
        public IEnumerable<T> X { get; }
        public IEnumerable<E> Y { get; }
        public IEnumerable<(T, E)> Data { get; }

    }
}
