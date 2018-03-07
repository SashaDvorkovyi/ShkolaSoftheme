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
            if (_lastElement == _arrayKey.Length)
            {

            }
        }
    }
}
