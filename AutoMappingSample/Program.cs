using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using AutoMappingSample.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

namespace AutoMappingSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //string connString =
            //@"Data Source=.;Initial Catalog=NH3BeginnersGuide;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var cfg = new OrderingSystemConfiguration();
            var configuration = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012)
            .Mappings(m => m.AutoMappings.Add(
            AutoMap.AssemblyOf<Customer>(cfg)))
            .BuildConfiguration();

            var exporter = new SchemaExport(configuration);
            exporter.Execute(true, false, false);

            Console.Write("Hit enter to exit:");
            Console.ReadLine();
        }
}
}
