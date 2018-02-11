using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cloning
{
    public struct UserStruct
    {
        public string firstName { get; }
        public string lastName { get; }
        public int id { get; }

        public UserStruct(string firstName, string lastName)
        {
            this.id = IDNumber.idTake();
            this.firstName = firstName;
            this.lastName = lastName;
        }


        public UserStruct clone()
        {
            UserStruct str1 = new UserStruct(this.firstName, this.lastName);
            return str1;
        }
    }
}
