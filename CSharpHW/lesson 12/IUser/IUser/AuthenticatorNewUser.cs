using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUser
{
    public class AuthenticatorNew : IAuthenticator
    {

        public IUser AuthenticateUser(ref List<IUser> users, IUser user)
        {
            IUser resalt = null;
            var IUserFind = false;

            if (users.Count == 0)
            {
                users.Add(user);
                IUserFind = true;
            }
            else
            {
                foreach (var item in users)
                {
                    if (item.Name == user.Name && item.Email == user.Email)
                    {
                        IUserFind = true;
                    }
                }
            }
            if (IUserFind != true)
            {
                users.Add(user);
            }
            return resalt;
        }

    }
}
