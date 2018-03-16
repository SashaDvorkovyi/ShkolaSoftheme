using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_operator
{
    class MobileOperator
    {


        private List<MobileAccount> _listAccount;
        private List<int[]> _magazine;



        public MobileOperator()
        {
            _listAccount = new List<MobileAccount>();
            _magazine = new List<int[]>();
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
            var account = sender as MobileAccount;
            if (account != null)
            {
                foreach (var item in _listAccount)
                {
                    if (e.Number == item.Number)
                    {
                        if (e.Message==null)
                        {
                            _magazine.Add(new int[] { account.Number, e.Number, 2 });
                            item.Show(sender, e);
                            break;
                        }
                        else
                        {
                            _magazine.Add(new int[] { account.Number, e.Number, 1 });
                            item.Show(sender, e);
                            break;
                        }

                    }
                }
            }
 
        }
        public void Top_5_Outgoing()
        {
            var result = new List<int>();
            var t =
                from i in _magazine
                group i by i[0]
                   into g

        }
    }
}
