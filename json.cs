using System;
using System.IO;
using System.Data.SqlClient;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "-h" || args[0] == "--help")
            {
                DisplayHelp();
            }
            else if (args.Length == 2 && args[0] == "-j")
            {
                string jsonFilePath = args[1];
                InsertJsonDataIntoDatabase(jsonFilePath);
            }
            else if (args.Length == 1 && args[0] == "-w")
            {
                Console.WriteLine("Hello, world!");
            }
            else
            {
                Console.WriteLine("Invalid arguments. Use -h or --help for help.");
            }
        }

        static void DisplayHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  -h, --help             Display help screen.");
            Console.WriteLine("  -j [jsonFilePath]      Read content of a JSON file and insert into database table.");
            Console.WriteLine("  -w                     Display 'Hello, world!'");
        }

        static void InsertJsonDataIntoDatabase(string jsonFilePath)
        {
            // Read JSON file
            string jsonData = File.ReadAllText(jsonFilePath);

            // Insert JSON data into database table
            string connectionString = "Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;";
            string insertSql = "INSERT INTO MyTable (JsonData) VALUES (@JsonData)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);
                command.Parameters.AddWithValue("@JsonData", jsonData);
                connection.Open();

                // Show spinning progress bar while SQL insert is happening
                const int totalTicks = 10;
                for (int i = 0; i <= totalTicks; i++)
                {
                    string message = $"Inserting data into database... {i * 10}%";
                    Console.Write($"\r{message} [{new string('#', i)}{new string('-', totalTicks - i)}]");
                    Thread.Sleep(100);
                }
                Console.WriteLine();

                command.ExecuteNonQuery();
            }
        }
    }
}
