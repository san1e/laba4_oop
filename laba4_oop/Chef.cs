using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4_oop
{
    public class Chef
    {
        private string _firstName;
        private string _lastName;

        public Chef(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}



