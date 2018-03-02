using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    public class LuckyNumber
    {
        public string LukyNumberIs { get; private set; }
        public const int Lenght = 6;

        public LuckyNumber()
        {
            LukyNumberGenerate();
        }

        public string this[int index]
        {
            get => LukyNumberIs[index].ToString();
            set =>throw new NotImplementedException();
        }

        public string LukyNumberGenerate()
        {
            var variable = new Random();
            LukyNumberIs = string.Empty;
            for (var i=0; i<Lenght; i++)
            {
                var number = variable.Next(1, 9);
                LukyNumberIs += number.ToString();
            }
            return LukyNumberIs;
        }


    }
}
