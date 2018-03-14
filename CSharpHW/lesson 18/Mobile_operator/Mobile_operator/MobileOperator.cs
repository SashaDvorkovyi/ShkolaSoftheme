using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_operator
{
    class MobileOperator
    {


        public List<MobileAccount> listAccount;

        public MobileOperator()
        {
            listAccount = new List<MobileAccount>();

        }

        public void AcceptAndSend(object sender, EEventArgs e)
        {
            var account = sender as MobileAccount;
            if (account != null)
            {
                foreach (var item in listAccount)
                {
                    if (account.Number == item.Number)
                    {
                        if (e.Message != null)
                        {
                            item.MessageEvent += item.Show;
                        }
                        else
                        {
                            item.CallEvent+= item.Show;
                        }
                    }
                }
            }

        }
                    


    }
}
