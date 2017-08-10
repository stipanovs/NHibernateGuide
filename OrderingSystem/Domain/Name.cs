using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingSystem.Domain
{
    public class Name
    {
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }

        public Name(string firstName, string middleName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name must be defined.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name must be defined.");

            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = FirstName.GetHashCode();
                result = (result * 397) ^ (MiddleName != null ?
                MiddleName.GetHashCode() : 0);
                result = (result * 397) ^ LastName.GetHashCode();
                return result;
            }
        }

        public bool Equals(Name other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.FirstName, FirstName) &&
            Equals(other.MiddleName, MiddleName) &&
            Equals(other.LastName, LastName);
        }

        public override bool Equals(object other)
        {
            return Equals(other as Name);
        }
    }
}
