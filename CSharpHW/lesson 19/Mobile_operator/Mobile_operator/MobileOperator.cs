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
        private Dictionary<int, DataCallEndMessage> _magazine;
        public MobileOperator()
        {
            _listAccount = new List<MobileAccount>();
            _magazine = new Dictionary<int, DataCallEndMessage>();
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
                _magazine.Add(number, new DataCallEndMessage());
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
                    _magazine.Remove(i);
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
                            _magazine[e.Number].InCall+=2;
                            _magazine[account.Number].OutCall += 2;
                            item.Show(sender, e);
                            break;
                        }
                        else
                        {
                            _magazine[e.Number].InCall ++;
                            _magazine[account.Number].OutCall ++;
                            item.Show(sender, e);
                            break;
                        }

                    }
                }
            }
 
        }

        public void Top_5_Outgoing()
        {
            var result = _magazine.OrderBy(x =>- x.Value.OutCall - x.Value.OutMessage).Take(5);
            foreach(var item in result)
            {
                Console.WriteLine(item.Key);
            }
        }

        public void Top_5_Ingoing()
        {
            var result = _magazine.OrderBy(x => -x.Value.InCall - x.Value.InMessage).Take(5);
            foreach (var item in result)
            {
                Console.WriteLine(item.Key);
            }
        }
    }
}
