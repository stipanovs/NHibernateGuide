using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMappingSample.Domain
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual IList<LineItem> LineItems { get; set; }
    }
}
