using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingSystem.Domain
{
    public abstract class Entity<T> where T : Entity<T>
    {
        public int Id { get; private set; }
        private int? oldHashCode;
        public override bool Equals(object obj)
        {
            var other = obj as T;
            if (other == null) return false;

            var thisIsNew = Equals(Id, 0);
            var otherIsNew = Equals(other.Id, 0);
            if (thisIsNew && otherIsNew)
                return ReferenceEquals(this, other);

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            // once we have a hashcode we'll never change it
            if (oldHashCode.HasValue)
                return oldHashCode.Value;
            // when this instance is new we use the base hash code
            // and remember it, so an instance can NEVER change its
            // hash code.
            var thisIsNew = Equals(Id, 0);
            if (thisIsNew)
            {
                oldHashCode = base.GetHashCode();
                return oldHashCode.Value;
            }
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<T> lhs, Entity<T> rhs)
        {
            return Equals(lhs, rhs);
        }
        public static bool operator !=(Entity<T> lhs, Entity<T> rhs)
        {
            return !Equals(lhs, rhs);
        }
    }


}
