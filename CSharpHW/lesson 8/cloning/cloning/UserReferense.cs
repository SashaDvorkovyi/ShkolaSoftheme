using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cloning
{
    public class UserReferense
    {
        private string _firstName;
        private string _lastName;
        private int _id;

        public UserReferense(string firstName, string lastName)
        {
            _id = IDNumber.idTake();
            _firstName = firstName;
            _lastName = lastName;
        }
        private UserReferense()
        {
            _id = IDNumber.idTake();
        }

        public UserReferense clone()
        {
            UserReferense ob1 = new UserReferense();
            ob1._firstName = this._firstName;
            ob1._lastName = this._lastName;
            return ob1;
        }
        public void show()
        {
            Console.WriteLine("UserReferense: First name: {0}. Last name: {1}. ID: {2}", _firstName, _lastName, _id);
        }
    }
}
