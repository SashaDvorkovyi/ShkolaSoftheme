using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUser
{
    class AuthenticatorNoisyUsers : IAuthenticator
    {
        public IUser AuthenticateUser(ref List<IUser> users, IUser user)
        {
            IUser resalt = null;
            if (users.Count == 0)
            {
            }
            else
            {
                foreach (var item in users)
                {
                    if ( user.Name ==null)
                    {
                        if (item.Email == user.Email && item.Password == user.Password)
                        {
                            resalt = item;
                            item.LastEntryCheck();
                        }
                    }
                    else
                    {
                        if (item.Name == user.Name && item.Password == user.Password)
                        {
                            resalt = item;
                            item.LastEntryCheck();
                        }
                    }
                }
            }
            return resalt;
        }
    }
}
