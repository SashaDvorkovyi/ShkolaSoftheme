using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArrey
{
    public class List<T>
    {
        private T[] _arrey;
        private int _lastElement=default(int);
        private int _lenght = 10;

        public List()
        {
            _arrey = new T[_lenght];
        }
        private List(int _lenght)
        {
            _arrey = new T[_lenght];
        }

        public void Add(T item)
        {
            if (_lenght == _lastElement)
            {
                _lenght *= 2;
                List<T> newLisr= new List<T>(_lenght);
                for(var i=0; i<this._lastElement; i++)
                {
                    newLisr._arrey[i] = this._arrey[i];
                }
                this._arrey = newLisr._arrey;
            }
            _arrey[_lastElement] = item;
            _lastElement++;
        }

        public int Lenght()
        {
            return _lastElement;
        }
        public T GetByIndex(int index)
        {
            if (index > _lastElement - 1)
            {
                return default(T);
            }
            return _arrey[index-1];
        }

        public bool Contains(T item)
        {
            bool answer = false;
            for(int i=0; i<_lastElement; i++)
            {
                if ( string.Compare( _arrey[i].ToString(), item.ToString())==0)
                {
                    answer = true;
                }
            }
            return answer;
        }
        public void Show()
        {
            for (var i = 0; i < _lastElement; i++)
            {
                Console.Write(_arrey[i] + ", ");
            }
        }
    }
}
