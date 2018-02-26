using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUser
{
    public interface IUserDataBase : IDisposable
    {
        void AllUsersGet();
        void SearchUserName(string name);
        void AddUser(IUser user);

    }
}
