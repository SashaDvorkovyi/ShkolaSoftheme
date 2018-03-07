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
            for (int i = 0; i < elements.Length; i++)
            {
                _array[i] = elements[i];
            }
            _lastElement = elements.Length;
        }

        public T Pop()
        {
            var resalt = default(T);
            if(_lastElement != 0)
            {
                resalt = _array[_lastElement - 1];
                _array[_lastElement - 1] = default(T);
                _lastElement--;
            }
            return resalt;
        }

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
