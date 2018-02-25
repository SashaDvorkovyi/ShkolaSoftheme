using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUser
{
    public interface IAuthenticator
    {
        IUser AuthenticateUser(UserDataBase userDataBase, IUser user);
    }
}
