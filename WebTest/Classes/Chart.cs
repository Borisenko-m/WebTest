using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Classes
{
    public class Chart<T, E>
    {
        HashSet<DataLayer> DataLayers { get; }

        public DataLayer Layer(string label)
        => DataLayers.First(l => l.Label.Equals(label));

        public void AddLayer(IEnumerable<(T, E)> layer)
        => (DataLayers as List<DataLayer>).Add(new DataLayer(layer));

        public bool RemoveLayer(IEnumerable<(T, E)> layer)
        => (DataLayers as List<DataLayer>).Remove(new DataLayer(layer));

        public bool RemoveLayer(DataLayer layer)
        => (DataLayers as List<DataLayer>).Remove(layer);

        public bool RemoveLayer(string label)
        => 0 > (DataLayers as List<DataLayer>).RemoveAll(l => l.Label == label);


        public class DataLayer
        {
            public DataLayer(IEnumerable<(T, E)> data) { Data = data; }

            public string Label { get; set; }
            public IEnumerable<(T, E)> Data { get; }
            public IEnumerable<T> X => Data.Select(l => l.Item1);
            public IEnumerable<E> Y => Data.Select(l => l.Item2);

            public override bool Equals(object obj)
            {
                return obj.ToString() == this.ToString();
            }

            public override string ToString()
            {
                return GetHashCode().ToString();
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
    }
}
