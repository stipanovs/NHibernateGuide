using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMappingSample.Domain
{
    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string CustomerName { get; set; }
    }
}
