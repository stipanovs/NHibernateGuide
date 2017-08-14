using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMappingSample.Domain
{
    public class LineItem 
    {
        public virtual int Id { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual string ProductCode { get; set; }

    }
}
