using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUser
{
    class AuthenticatorNoisyUsers : IAuthenticator
    {
        public IUser AuthenticateUser(UserDataBase userDataBase, IUser user)
        {
            IUser resalt = null;
            if (userDataBase.SearchUser(ref user))
            {
                resalt = user;
            }
            return resalt;
        }
    }
}
