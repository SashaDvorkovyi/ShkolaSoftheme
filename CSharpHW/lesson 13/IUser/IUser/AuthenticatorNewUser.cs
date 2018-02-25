using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUser
{
    public class AuthenticatorNew : IAuthenticator
    {

        public IUser AuthenticateUser(UserDataBase userDataBase, IUser user)
        {
            IUser resalt = null;
      
            if (!userDataBase.SearchUserForNew(user))
            {
                userDataBase.AddUser(user);
                resalt = user;
            }
  
            return resalt;
        }

    }
}
