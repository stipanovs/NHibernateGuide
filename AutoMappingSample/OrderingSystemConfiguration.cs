using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMappingSample.Domain;
using FluentNHibernate.Automapping;


namespace AutoMappingSample 
{
    public class OrderingSystemConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == typeof(Customer).Namespace;
        }
    }
}
