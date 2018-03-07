using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class MyDictionary<TKey, TValue>
    {
        private TKey[] _arrayKey;
        private TValue[] _arrayValue;
        private int _lastElement;

        public MyDictionary()
        {
            _arrayKey = new TKey[10];
            _arrayValue = new TValue[10];
            _lastElement = 0;
        }
        public void Add(TKey key, TValue value)
        {
            var add = true;
            foreach(var i in _arrayKey)
            {
                if(key.Equals(i))
                {
                    add = false;
                }
            }
            if (add==true)
            {
                if (_lastElement == _arrayKey.Length)
                {
                    var newArrayKey = new TKey[_arrayKey.Length * 2];
                    Array.Copy(_arrayKey, newArrayKey, _arrayKey.Length);
                    _arrayKey = newArrayKey;
                    var newArrayValue = new TValue[_arrayValue.Length * 2];
                    Array.Copy(_arrayValue, newArrayValue, _arrayValue.Length);
                    _arrayValue = newArrayValue;
                }
                _arrayKey[_lastElement] = key;
                _arrayValue[_lastElement++] = value;
            }
        }
        public void Remove(TKey key)
        {
            var delete = default(bool);
            var i = default(int);
            for (var t=0; t<_lastElement; t++)
            {
                if (key.Equals(_arrayKey[t]))
                {
                    delete = true;
                    i = t;
                    break;
                }
            }
            if (delete == true)
            {
                _arrayKey[i] = default(TKey);
                _arrayValue[i] = default(TValue);
                for (var t=i+1; t<_lastElement; i++)
                {
                    _arrayKey[t - 1] = _arrayKey[t];
                    _arrayValue[t - 1] = _arrayValue[t];
                }
                _lastElement--;
            }
        }
        public void Sorted()
        {
            var keyNew = new TKey[_lastElement];
            Array.Copy(_arrayKey, keyNew, _lastElement);
            var valueBefore = new TValue[_lastElement];
            Array.Copy(_arrayValue, valueBefore, _lastElement);
            Array.Sort(keyNew); 
            for(var i=0; i<_lastElement; i++)
            {
                for (var t = 0; t < _lastElement; t++)
                {
                    if (keyNew[i].Equals(_arrayKey[t]))
                    {
                        _arrayValue[i] = valueBefore[t];
                        break;
                    }
                }
            }
            Array.Copy(keyNew, _arrayKey, _lastElement);
        }
        public TValue GetValue(TKey key)
        {
            var resalt = default(TValue);
            for(var i=0; i<_lastElement; i++)
            {
                if (key.Equals(_arrayKey[i]))
                {
                    resalt = _arrayValue[i];
                } 
            }
            return resalt;
        }
    }
}
