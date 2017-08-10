using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingSystem.Domain
{
    public class Order : Entity<Order>
    {
        private readonly List<Order> orders;
        public IEnumerable<Order> Orders { get { return orders; } }

        public Customer Customer { get; private set; }

        public Employee Reference { get; private set; }

        public DateTime OrderDate { get; private set; }
        public decimal OrderTotal { get; private set; }

        private readonly List<LineItem> lineItems;
        public IEnumerable<LineItem> LineItems { get { return lineItems; } }

        public Order(Customer customer)
        {
            lineItems = new List<LineItem>();
            Customer = customer;
            OrderDate = DateTime.Now;
        }

        public void AddProduct(Customer customer, Product product, int quantity)
        {
            Customer = customer;
            var line = new LineItem(this, quantity, product);
            lineItems.Add(line);
        }
    }
}
