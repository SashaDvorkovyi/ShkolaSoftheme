using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_operator
{
    class MobileOperator
    {


        private List<MobileAccount> listAccount;

        public MobileOperator()
        {
            listAccount = new List<MobileAccount>();
        }

        public bool AddAAccount(int number)
        {
            var result = true;
            foreach(var item in listAccount)
            {
                if (number==item.Number)
                {
                    result = false;
                    break;
                }
            }
            if (result)
            {
                listAccount.Add(new MobileAccount(number));
                listAccount[listAccount.Count - 1].MessageEvent += AcceptAndSend;
                listAccount[listAccount.Count - 1].CallEvent += AcceptAndSend;
            }
            return result;
        }

        public void DeleteAccount(int number)
        {
            for (var i=0; i<listAccount.Count; i++)
            {
                if (number == listAccount[i].Number)
                {
                    listAccount.RemoveAt(i);
                    listAccount[i].MessageEvent -= AcceptAndSend;
                    listAccount[i].CallEvent -= AcceptAndSend;
                }
            }
        }

        public MobileAccount TakeAccount(int number)
        {
            foreach (var item in listAccount)
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
            foreach (var item in listAccount)
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
