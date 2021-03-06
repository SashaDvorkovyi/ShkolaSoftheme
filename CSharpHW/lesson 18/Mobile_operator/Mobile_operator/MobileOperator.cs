﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_operator
{
    class MobileOperator
    {


        private List<MobileAccount> _listAccount;

        public MobileOperator()
        {
            _listAccount = new List<MobileAccount>();
        }

        public bool AddAAccount(int number)
        {
            var result = true;
            foreach(var item in _listAccount)
            {
                if (number==item.Number)
                {
                    result = false;
                    break;
                }
            }
            if (result)
            {
                _listAccount.Add(new MobileAccount(number));
                _listAccount[_listAccount.Count - 1].MessageEvent += AcceptAndSend;
                _listAccount[_listAccount.Count - 1].CallEvent += AcceptAndSend;
            }
            return result;
        }

        public void DeleteAccount(int number)
        {
            for (var i=0; i<_listAccount.Count; i++)
            {
                if (number == _listAccount[i].Number)
                {
                    _listAccount.RemoveAt(i);
                    _listAccount[i].MessageEvent -= AcceptAndSend;
                    _listAccount[i].CallEvent -= AcceptAndSend;
                }
            }
        }

        public MobileAccount TakeAccount(int number)
        {
            foreach (var item in _listAccount)
            {
                if (number == item.Number)
                {
                    return item;
                }
            }
            return null;
        }

        public void AcceptAndSend(object sender, EEventArgs e)
        {
            foreach (var item in _listAccount)
            {
                if (e.Number == item.Number)
                {
                    item.Show(sender, e);
                    break;
                }
            }
        }
    }
}
