using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace AdoNetExample
{
    class Program
    {
        static void Main(string[] args)
        {

            //CreateCountryTable();
            //CreateCityTable();

            Console.WriteLine("create table");
        }

        private static void CreateCityTable()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var commandCreateTable =
                        @"CREATE TABLE City( 
                        Id int identity(1,1) primary key,
                        Name varchar(70),
                        Country_id int,
                        CONSTRAINT city_country_id FOREIGN KEY (country_id)
                        REFERENCES Country(id) ON DELETE NO ACTION);";
                using (var sqlcommand = new SqlCommand(commandCreateTable, sqlConnection))
                {
                    sqlcommand.ExecuteNonQuery();
                }
            }
        }

        private static void CreateCountryTable()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var commandCreateTable =
                        @"CREATE TABLE Country(
                        id int identity(1,1) primary key,
                        name varchar(70),
                        num_code int); ";
                using (var sqlcommand = new SqlCommand(commandCreateTable, sqlConnection))
                {
                    sqlcommand.ExecuteNonQuery();
                }
            }
        }
    }
}
