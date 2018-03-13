using System.Collections.Generic;

namespace Iterator
{
    class ConcreteAggregate<T> : Aggregate<T>
    {
        private readonly List<T> _items = new List<T>();

        public override Iterator<T> CreateIterator()
        {
            return new ConcreteIterator<T>(this);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public T this[int index]
        {
            get { return _items[index]; }
            set { _items.Insert(index, value); }
        }
    }
}