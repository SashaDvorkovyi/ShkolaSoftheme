using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUser
{
    class User : IUser
    {
        public string Name { get; }
        public string Password { get; }
        public string Email { get; }
        private DateTime _lastEntrance;
        public  DateTime LastEntrance
        { get
            {
                return _lastEntrance;
            }
        }


        public User(string name, string password, string email)
        {
            Name = name;
            Password = password;
            Email = email;
            _lastEntrance = DateTime.Now;
        }


        public string GetFullInfo()
        {
            var resalt = string.Empty;
            resalt= "Name: "+ Name +". Psssword: "+ Password +". Email: "+ Email +". Last entrance: "+ LastEntrance;
            return resalt;
        }
        public void LastEntryCheck()
        {
                _lastEntrance = DateTime.Now;
        }
    }
}
