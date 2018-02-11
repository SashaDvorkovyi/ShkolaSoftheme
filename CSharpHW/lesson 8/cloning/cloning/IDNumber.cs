using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cloning
{
    public class IDNumber
    {
        private static int? _idNumber;

        public static int idTake()
        {
            if (_idNumber == null)
            {
                _idNumber = 0;
            }
            _idNumber++;
            return (int)_idNumber;
        }

    }
}
