using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryService
{
    public class Person
    {
        public string Login { get; set; }
        public byte[] Pass { get; set; }

        public override bool Equals(Object obj)
        {
            // If parameter is null return false:
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Person person = obj as Person;

            return Equals(person);
        }

        public bool Equals(Person person)
        {
            // If parameter is null return false:
            if (person == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Login == person.Login||Pass==person.Pass);
        }

        public override int GetHashCode()
        {
            return Login.GetHashCode()^Pass.GetHashCode();
        }
    }
}