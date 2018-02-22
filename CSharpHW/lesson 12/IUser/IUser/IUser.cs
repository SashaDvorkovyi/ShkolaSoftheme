using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUser
{
    public interface IUser
    {
        string Name { get; }
        string Password { get; }
        string Email { get; }
        DateTime LastEntrance { get; }

        string GetFullInfo();
        void LastEntryCheck();
    }
}
