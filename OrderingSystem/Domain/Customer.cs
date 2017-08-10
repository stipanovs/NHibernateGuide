using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingSystem.Domain
{
    public class Customer : Entity<Customer>
    {
        public string CustomerIdentifier { get; private set; }
        public Name CustomerName { get; private set; }

        public Address Address { get; private set; }

        private readonly List<Order> orders;
        public IEnumerable<Order> Orders { get { return orders; } }

        public void ChangeCustomerName(string firstName, string middleName, string lastName)
        {
            CustomerName = new Name(firstName, middleName, lastName);
        }

        public void PlaceOrder(LineInfo[] lineInfos, IDictionary<int, Product> products)
        {
            var order = new Order(this);
            foreach (var lineInfo in lineInfos)
            {
                var product = products[lineInfo.ProductId];
                order.AddProduct(this, product, lineInfo.Quantity);
            }
            orders.Add(order);
        }
    }
}
