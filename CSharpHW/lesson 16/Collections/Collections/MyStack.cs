using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class MyStack<T>
    {
        private T[] _array;
        private int _lastElement;

        public MyStack(params T[] elements)
        {
            _array = new T[elements.Length * 2];
            _lastElement = elements.Length;
        }

        public T Pop() => _lastElement != 0 ? _array[_lastElement-- - 1] : default(T);

        public void Push(T element)
        {
            if (_lastElement == _array.Length)
            {
                var newArray = new T[_array.Length * 2];
                Array.Copy(_array, newArray, _array.Length);
                _array = newArray;
            }
            _array[_lastElement++] = element;
        }

        public T Peek() => _lastElement != 0? _array[_lastElement-1] : default(T);
    }
}
