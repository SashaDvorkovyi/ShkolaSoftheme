using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class MyQueue<T>
    {
        private T[] _array;
        private int _lastElement;

        public MyQueue(params T[] elements)
        {
            _array = new T[elements.Length*2];
            _lastElement = elements.Length;
        }

        public T Dequeue()
        {
            var ansver = default(T);
            if (_lastElement > 0)
            {
                ansver = _array[0];
                for (var i = 1; i < _lastElement; i++)
                {
                    _array[i - 1] = _array[i];
                }
                _lastElement--;
            }
            return ansver;
        }

        public void Enqueue(T element)
        {
            if (_lastElement == _array.Length)
            {
                var newArray = new T[_array.Length*2];
                Array.Copy(_array, newArray, _array.Length);
                _array = newArray;
            }
            _array[_lastElement++] = element;
        }

        public T Peek() => _array[0]!=null? _array[0] : default(T);
    }
}
