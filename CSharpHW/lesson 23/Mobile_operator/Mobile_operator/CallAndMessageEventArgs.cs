﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_operator
{

    public class CallAndMessageEventArgs : EventArgs
    {
        public CallAndMessageEventArgs(int number, string message)
        {
            Message = message;
            Number = number;
        }

        public CallAndMessageEventArgs(int number)
        {
            Number = number;
        }

        public string Message { get; set; }

        public int Number { get; set; }
    }
}
