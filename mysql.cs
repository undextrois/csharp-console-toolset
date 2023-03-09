using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "help")
            {
                // Display help information
                Console.WriteLine("Usage: program.exe [mysql|mssql|help]");
            }
            else if (args[0] == "mysql")
            {
                // Connect to MySQL database
                string connectionString = "server=localhost;user=root;password=mypassword;database=mydatabase;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                try
                {
                    connection.Open();
                    Console.WriteLine("Connected to MySQL database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting to MySQL database: " + ex.Message);
                }
            }
            else if (args[0] == "mssql")
            {
                // Connect to Microsoft SQL Server database
                string connectionString = "Server=localhost;Database=mydatabase;User Id=myusername;Password=mypassword;";
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    Console.WriteLine("Connected to Microsoft SQL Server database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting to Microsoft SQL Server database: " + ex.Message);
                }
            }
            else
            {
                // Invalid argument
                Console.WriteLine("Invalid argument: " + args[0]);
            }
        }
    }
}
