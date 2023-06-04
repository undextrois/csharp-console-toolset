/*
csc -r:System.dll -r:System.Data.dll -r:MySql.Data.dll ask-db.cs
mono ask-db.exe

*/
using System;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace AskDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Ask-DB!");
            Console.WriteLine("Which database would you like to connect to? (mysql/mssql/sqldb)");

            string databaseOption = Console.ReadLine();

            switch (databaseOption.ToLower())
            {
                case "mysql":
                    ConnectToMySQL();
                    break;
                case "mssql":
                    ConnectToMSSQL();
                    break;
                case "sqldb":
                    ConnectToSQLDB();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select either mysql, mssql, or sqldb.");
                    break;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void ConnectToMySQL()
        {
            string connectionString = GetConnectionString("mysql");
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to MySQL database!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to MySQL database: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        static void ConnectToMSSQL()
        {
            string connectionString = GetConnectionString("mssql");
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to MSSQL database!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to MSSQL database: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        static void ConnectToSQLDB()
        {
            string connectionString = GetConnectionString("sqldb");
            // Connect to SQLDB implementation
            // Add your code here to connect to SQLDB
        }

        static string GetConnectionString(string database)
        {
            string credentialsPath = "credentials.txt"; // Path to your credentials file

            if (!File.Exists(credentialsPath))
            {
                Console.WriteLine("Credentials file not found!");
                Environment.Exit(0);
            }

            string[] credentials = File.ReadAllLines(credentialsPath);
			
			if (credentials.Length < 4)
			{
				Console.WriteLine("Invalid credentials file format.");
				Environment.Exit(0);
			}

            switch (database.ToLower())
            {
                case "mysql":
                    return $"Server={credentials[0]};Database={credentials[1]};Uid={credentials[2]};Pwd={credentials[3]};";
                case "mssql":
                    return $"Server={credentials[0]};Database={credentials[1]};User Id={credentials[2]};Password={credentials[3]};";
                case "sqldb":
                    // Return SQLDB connection string
                    return "";
                default:
                    return "";
            }
        }
    }
}
