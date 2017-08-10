using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingSystem.Domain
{
    public class Customer : Entity<Customer>
    {
        public string CustomerIdentifier { get; private set; }
        public Name CustomerName { get; private set; }

        public void ChangeCustomerName(string firstName, string middleName, string lastName)
        {
            CustomerName = new Name(firstName, middleName, lastName);
        }
    }
}
