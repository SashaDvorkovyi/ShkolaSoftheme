using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human
{
    class Human
    {
        public int birthDate { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; private set; }

        public Human()
        {
            this.age = 20;
        }
        public Human(int birthDate, string firstNamef, string lastName, int age)
        {
            this.birthDate = birthDate;
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
        }
        public Human(int birthDate, string firstNamef, string lastName) :this(birthDate, firstNamef, lastName, 20)
        {
        }

        public bool Equals(Human first, Human second)
        {
            var result = false;
            if ((first.birthDate==second.birthDate) && 0==string.Compare(first.firstName, second.firstName) && 0 == string.Compare(first.lastName, second.lastName) && (first.age == second.age))
            {
                result = true;
            }
            return result;
        }

    }
}
