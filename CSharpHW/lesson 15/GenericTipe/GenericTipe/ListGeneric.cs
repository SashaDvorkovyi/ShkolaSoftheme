using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericTipe
{
    class ListGeneric<T>
    {
        private T[] _array;
        private int _lenght;
        private const int _AddLenght= 10;

        public ListGeneric(params T[] element)
        {
            _array = new T[element.Length+ _AddLenght];
            _lenght = element.Length;
            for (var i = 0; i < element.Length; i++)
            {
                _array[i] = element[i];
            }
        }

        public T this[int index]
        {
            get =>_array[index]; 
            set
            {
                if (index < _lenght)
                {
                    _array[index] = value;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public void AddElement(params T[] element)
        {
            if (_lenght + element.Length > _array.Length)
            {
                var nawArray = new T[element.Length + _lenght+ _AddLenght];
                Array.Copy(_array, nawArray, _array.Length);
                _array = nawArray;
                var a = default(int);
                for (var i = _lenght; i < element.Length + _lenght; i++)
                {
                    _array[i] = element[a++];
                }
                _lenght += element.Length;
            }
            else
            {
                var a = default(int);
                for (var i = _lenght; i < element.Length + _lenght; i++)
                {
                    _array[i] = element[a++];
                }
                _lenght += element.Length;
            }

        }

        public void DeleteElement(T obj)
        {
            for(var i =0; i<_lenght; i++)
            {
                if (obj.Equals(_array[i]))
                {
                    for (var t = i; t < _lenght; t++)
                    {
                        _array[t - 1] = _array[t];
                    }
                    _lenght--;
                    break;
                }
            }
        }

        public void DeleteElement(int elementNumber)
        {
            if (0 < elementNumber && elementNumber < _lenght)
            {
                for (var i = elementNumber; i < _lenght; i++)
                {
                    _array[i - 1] = _array[i];
                }
                _lenght--;
            }
        }

        //public void [int i].DeleteElement()
        //{
        //}

        //public void DeleteElement(ListGeneric<T> obj[int i])
        //{
        //}

        public int Lenght() => _lenght;

        public bool Equals(T obj)
        {
            var resalt = default(bool);
            foreach(var item in _array)
            {
                if (obj.Equals(item))
                {
                    resalt = true;
                    break;
                }
            }
            return resalt;
        }

        public T[] ToArray()
        {
            if (_array.Length != _lenght)
            {
                var newArray = new T[_lenght];
                Array.Copy(_array, newArray, _lenght);
                return newArray;
            }
            else
            {
                return _array;
            }
        }
    }
}
