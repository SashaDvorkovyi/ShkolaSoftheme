using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUser
{
    public class Authenticator : IAuthenticator
    {
        private List<IUser> users;

        public Authenticator()
        {
            this.users = new List<IUser>();
        }

        public IUser AuthenticateUser(IUser user)
        {
            IUser resalt = null;
            if (users.Count == 0)
            {
            }
            else
            {
                foreach (var item in this.users)
                {
                    if (item.Name == user.Name && item.Email == user.Email)
                    {
                        resalt = user;
                    }
                    else if (item.Name == user.Name && item.Password == user.Password)
                    {
                        resalt = user;
                        user.LastEntryCheck();
                    }
                    else if (item.Email == user.Email && item.Password == user.Password)
                    {
                        resalt = user;
                        user.LastEntryCheck();
                    }
                }
            }

            return resalt;
        }

        public void AddUser(IUser user)
        {
            if (AuthenticateUser(user)==null)
            {
                users.Add(user);
            }
        }
    }
}
