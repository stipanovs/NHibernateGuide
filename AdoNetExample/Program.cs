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
            #region Create two Tables

            //CreateCountryTable();
            //CreateCityTable();
            //Console.WriteLine("create table");

            #endregion

            #region SQLDataReader

            //SelectAllCity();
            //Console.WriteLine("======");
            //SelectCountryWhereIdEqual_2(); // QueryParameters
            //Console.WriteLine("======");
            //CountCitys();
            //Console.ReadKey();

            #endregion

            #region Create DataTable, DataSet, DataAdapter SELECT, INSERT, UPDATE, DELETE

            DataTable cityTable = CreateDataTableCity();
            DataTable countryTable = CreateDataTableCountry();

            DataSet dataSet = new DataSet("DbWorld");
            dataSet.Tables.Add(cityTable);
            dataSet.Tables.Add(countryTable);

            dataSet.Relations.Add("CountryCity",
                dataSet.Tables["Country"].Columns["Id"],
                dataSet.Tables["City"].Columns["Country_id"]);


            //SelectDataAdapterCountry();
            //SelectDataAdapterCity();

            //InsertDataAdapterCountry(5, "Ucraina", 980);
            //InsertDataAdapterCity(1, "Kiev", 5);

            //UpdateDataAdapterCountry(5, "Ucraine", 980);
            //UpdateDataAdapterCity(1, "Kiev Gorod");

            //DeleteDataAdapterCity(1);
            //DeleteDataAdapterCountry(2);

            #endregion
            Console.ReadKey();
        }

        private static void DeleteDataAdapterCountry(int id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var deleteText = $"DELETE FROM Country WHERE Id = @Id";
                var selectText = $"Select * from Country";

                var adapter = new SqlDataAdapter();
                var deleteCommand = new SqlCommand(deleteText, sqlConnection);
                var selectCommand = new SqlCommand(selectText, sqlConnection);

                deleteCommand.Parameters.AddWithValue("@Id", id);

                adapter.SelectCommand = selectCommand;
                adapter.DeleteCommand = deleteCommand;

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataTable.PrimaryKey = new[] { dataTable.Columns["Id"] };
                dataTable.Rows.Find(id)?.Delete();
                adapter.Update(dataTable);
                Console.WriteLine($"An item has been deleted in Country.");
            }
        }

        private static void SelectDataAdapterCity()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;
            var commandText = "Select * from City";
            var sqlDataAdapter = new SqlDataAdapter(commandText, connectionString);

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            Console.WriteLine("Selecting from City ...");
            foreach (DataRow dataRow in dataSet.Tables["Table"].Rows)
            {
                Console.WriteLine($"Id :{dataRow["Id"]} Name : {dataRow["name"]} Country Id: {dataRow["Country_id"]}");
            }
        }

        private static void SelectDataAdapterCountry()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;
            var commandText = "Select * from Country";
            var sqlDataAdapter = new SqlDataAdapter(commandText, connectionString);

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            Console.WriteLine("Selecting from Country ...");
            foreach (DataRow dataRow in dataSet.Tables["Table"].Rows)
            {
                Console.WriteLine($"Id :{dataRow["Id"]} Name : {dataRow["name"]} Country Code: {dataRow["num_code"]}");
            }

        }

        private static void DeleteDataAdapterCity(int id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var deleteText = $"DELETE FROM City WHERE Id = @Id";
                var selectText = $"Select * from City";

                var adapter = new SqlDataAdapter();
                var deleteCommand = new SqlCommand(deleteText, sqlConnection);
                var selectCommand = new SqlCommand(selectText, sqlConnection);

                deleteCommand.Parameters.AddWithValue("@Id", id);

                adapter.SelectCommand = selectCommand;
                adapter.DeleteCommand = deleteCommand;

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataTable.PrimaryKey = new[] { dataTable.Columns["Id"] };
                dataTable.Rows.Find(id)?.Delete();
                adapter.Update(dataTable);
                Console.WriteLine($"An item has been deleted in City.");
            }

        }

        private static void UpdateDataAdapterCity(int id, string name)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var updateText = $"UPDATE City SET Name = @Name WHERE Id = @Id;";
                var selectText = "Select * from City";

                var adapter = new SqlDataAdapter();
                var updateCommand = new SqlCommand(updateText, sqlConnection);
                var selectCommand = new SqlCommand(selectText, sqlConnection);


                updateCommand.Parameters.AddWithValue("@Id", id);
                updateCommand.Parameters.AddWithValue("@Name", name);
                
                adapter.SelectCommand = selectCommand;
                adapter.UpdateCommand = updateCommand;

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataTable.PrimaryKey = new[] { dataTable.Columns["Id"] };
                var newRow = dataTable.Rows.Find(id);

                newRow["Id"] = id;
                newRow["Name"] = name;
             
                adapter.Update(dataTable);

                Console.WriteLine("A new item has been updated in City.");

            }
        }

        private static void UpdateDataAdapterCountry(int id, string name, int countryCode)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var updateText = $"UPDATE Country SET Name = @Name, num_code=@num_code WHERE Id = @Id;";
                var selectText = "Select * from Country";

                var adapter = new SqlDataAdapter();
                var updateCommand = new SqlCommand(updateText, sqlConnection);
                var selectCommand = new SqlCommand(selectText, sqlConnection);
                                

                updateCommand.Parameters.AddWithValue("@Id", id);
                updateCommand.Parameters.AddWithValue("@name", name);
                updateCommand.Parameters.AddWithValue("@num_code", countryCode);

                adapter.SelectCommand = selectCommand;
                adapter.UpdateCommand = updateCommand;

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataTable.PrimaryKey = new[] { dataTable.Columns["Id"] };
                var newRow = dataTable.Rows.Find(id);
                                               
                newRow["Id"] = id;
                newRow["name"] = name;
                newRow["num_code"] = countryCode;
               
                adapter.Update(dataTable);

                Console.WriteLine("A new item has been updated in Country.");
               
            }
        }

        private static void InsertDataAdapterCity(int id, string name, int countryId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand constrain = new SqlCommand("SELECT COUNT(*) FROM [City] WHERE ([Id] = @Id)", sqlConnection);
                constrain.Parameters.AddWithValue("@Id", id);
                if (((int)constrain.ExecuteScalar()) == 0)
                {

                    var insertText = $"INSERT INTO City (Id, Name, Country_id) VALUES (@Id, @Name, @Country_id);";
                    var selectText = "Select * from City";

                    var adapter = new SqlDataAdapter();
                    var insertCommand = new SqlCommand(insertText, sqlConnection);
                    var selectCommand = new SqlCommand(selectText, sqlConnection);

                    var identityInsertOn = new SqlCommand("SET IDENTITY_INSERT [City] ON;", sqlConnection);
                    identityInsertOn.ExecuteNonQuery();

                    insertCommand.Parameters.AddWithValue("@Id", id);
                    insertCommand.Parameters.AddWithValue("@Name", name);
                    insertCommand.Parameters.AddWithValue("@Country_id", countryId);

                    adapter.SelectCommand = selectCommand;
                    adapter.InsertCommand = insertCommand;

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DataRow newRow = dataTable.NewRow();
                    newRow["Id"] = id;
                    newRow["Name"] = name;
                    newRow["Country_id"] = countryId;
                    dataTable.Rows.Add(newRow);

                    adapter.Update(dataTable);

                    var identityInsertOff = new SqlCommand("SET IDENTITY_INSERT [City] OFF;", sqlConnection);
                    identityInsertOff.ExecuteNonQuery();

                    Console.WriteLine("A new item has been added to City.");
                }
                else Console.WriteLine("Id already exists!");
            }
        }

        private static void InsertDataAdapterCountry(int id, string name, int countryCod)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand constrain = new SqlCommand("SELECT COUNT(*) FROM [Country] WHERE ([Id] = @Id)", sqlConnection);
                constrain.Parameters.AddWithValue("@Id", id);
                if (((int)constrain.ExecuteScalar()) == 0)
                {
                    
                    var insertText = $"INSERT INTO Country(Id, name, num_code) VALUES (@Id, @Name, @num_code);";
                    var selectText = "Select * from Country";

                    var adapter = new SqlDataAdapter();
                    var insertCommand = new SqlCommand(insertText, sqlConnection);
                    var selectCommand = new SqlCommand(selectText, sqlConnection);

                    var identityInsertOn = new SqlCommand("SET IDENTITY_INSERT [Country] ON;", sqlConnection);
                    identityInsertOn.ExecuteNonQuery();

                    insertCommand.Parameters.AddWithValue("@Id", id);
                    insertCommand.Parameters.AddWithValue("@name", name);
                    insertCommand.Parameters.AddWithValue("@num_code", countryCod);

                    adapter.SelectCommand = selectCommand;                       
                    adapter.InsertCommand = insertCommand;

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DataRow newRow = dataTable.NewRow();
                    newRow["Id"] = id;
                    newRow["name"] = name;
                    newRow["num_code"] = countryCod;
                    dataTable.Rows.Add(newRow);
                                
                    adapter.Update(dataTable);

                    var identityInsertOff = new SqlCommand("SET IDENTITY_INSERT [Country] OFF;", sqlConnection);
                    identityInsertOff.ExecuteNonQuery();

                    Console.WriteLine("A new item has been added to Country.");
                }
                else Console.WriteLine("Id already exists!");
            }
        }

        private static DataTable CreateDataTableCountry()
        {
            DataTable countryTable = new DataTable("Country");

            countryTable.Columns.Add("Id", typeof(int));
            countryTable.Columns.Add("name", typeof(string));
            countryTable.Columns.Add("num_code", typeof(int));

            // copy data from MSSQL in countryTable

            var connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var selectText = "Select * from Country";

                using (var sqlCommand = new SqlCommand(selectText, sqlConnection))
                {
                    var sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        DataRow row1 = countryTable.NewRow();
                        row1["Id"] = sqlDataReader["Id"];
                        row1["name"] = sqlDataReader["name"];
                        row1["num_code"] = sqlDataReader["num_code"];
                        countryTable.Rows.Add(row1);
                    }
                }
            }

            return countryTable;
        }

        private static DataTable CreateDataTableCity()
        {
            
            DataTable cityTable = new DataTable("City");
            DataColumn idColumn = new DataColumn("Id", typeof(int));

            cityTable.Columns.Add(idColumn);
            cityTable.Columns.Add("Name", typeof(string));
            cityTable.Columns.Add("Country_id", typeof(int));

            // copy data from MSSQL in CityTable

            var connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var selectText = "Select * from city";

                using (var sqlCommand = new SqlCommand(selectText, sqlConnection))
                {
                    var sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        DataRow row1 = cityTable.NewRow();
                        row1["Id"] = sqlDataReader["Id"];
                        row1["Name"] = sqlDataReader["Name"];
                        row1["Country_id"] = sqlDataReader["Country_id"];
                        cityTable.Rows.Add(row1);
                    }
                }
            }
            return cityTable;
        }

        public static void CountCitys()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var commandCreateTable =
                        @"SELECT COUNT(*) FROM City;";
                using (var sqlcommand = new SqlCommand(commandCreateTable, sqlConnection))
                {
                    int count = (int)sqlcommand.ExecuteScalar();
                    Console.WriteLine("Number of Citys is: " + count);
                }
            }
        }

        public static void CreateCityTable()
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
                        REFERENCES Country(id) ON DELETE SET NULL);";
                using (var sqlcommand = new SqlCommand(commandCreateTable, sqlConnection))
                {
                    sqlcommand.ExecuteNonQuery();
                }
            }
        }

        public static void CreateCountryTable()
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

        public static void SelectAllCity()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var selectText = "Select * from City";

                using (var sqlCommand = new SqlCommand(selectText, sqlConnection))
                {
                    var sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"Id:{sqlDataReader["id"]} Name: {sqlDataReader["Name"]}");
                    }
                }
            }
        }

        public static void SelectCountryWhereIdEqual_2()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdoNetDataBase"].ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var selectText = "Select * from Country Where Id = @Id";

                using (var sqlCommand = new SqlCommand(selectText, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int);
                    sqlCommand.Parameters["@Id"].Value = 2;

                    var sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"Id:{sqlDataReader["id"]} Name: {sqlDataReader["Name"]}");
                    }
                }
            }
        }



    }
}
