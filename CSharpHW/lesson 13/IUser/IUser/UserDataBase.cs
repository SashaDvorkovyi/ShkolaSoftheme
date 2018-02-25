using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUser
{
    public class UserDataBase : IUserDataBase, IDisposable
    {
        private List<IUser> _users;
        private bool _disposed;

        public UserDataBase()
        {
            _users = new List<IUser>();
        }

        public void AllUsersGet()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var user in _users)
            {
                Console.WriteLine(user.GetFullInfo());
            }
            Console.ResetColor();
        }
        public void SearchUserName(string name)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var user in _users)
            {
                if (string.Compare(user.Name, name) == 0)
                {
                    Console.WriteLine(user.GetFullInfo());
                }
            }
            Console.ResetColor();
        }
        public bool SearchUserForNew(IUser user)
        {
            return _users.Contains(user);
        }
        public bool SearchUser(ref IUser user)
        {
            bool resalt = false;
            foreach (var item in _users)
            {
                if (item.Name == user.Name && item.Password == user.Password)
                {
                    user = item;
                    item.LastEntryCheck();
                    resalt = true;
                }
                else if (item.Email == user.Email && item.Password == user.Password)
                {
                    user = item;
                    item.LastEntryCheck();
                    resalt = true;
                }
            }
            return resalt;
        }
        public void AddUser(IUser user)
        {
            _users.Add(user);
        }
        public void Dispose()
        {

            if (!this._disposed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                foreach(var item in _users)
                {
                    Console.WriteLine(item.GetFullInfo());
                }
                    Console.ResetColor();
                _users = null;
            }
            this._disposed = true;
        }
    }
}
