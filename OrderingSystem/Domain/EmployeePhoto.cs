using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingSystem.Domain
{
    class EmployeePhoto : Entity<EmployeePhoto>
    {
        public Employee Employee { get; private set; }
        public string ImageTitle { get; private set; }
        public string Description { get; private set; }
    }

}
