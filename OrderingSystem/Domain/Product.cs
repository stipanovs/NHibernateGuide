using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingSystem.Domain
{
    public class Product : Entity<Product>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int ReorderLevel { get; private set; }
        public bool Discontinued { get; private set; }
        // serii
        
    }
}
