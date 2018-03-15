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
