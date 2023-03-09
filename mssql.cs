using System;
using System.Data.SqlClient;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "help")
            {
                // Display help information
                Console.WriteLine("Usage: program.exe [help|sync|replace]");
            }
            else if (args[0] == "sync")
            {
                // Sync records between two database tables
                string sourceConnectionString = "Server=source_server;Database=source_db;User Id=source_user;Password=source_password;";
                string destinationConnectionString = "Server=destination_server;Database=destination_db;User Id=destination_user;Password=destination_password;";
                string sourceTable = "source_table";
                string destinationTable = "destination_table";

                SqlConnection sourceConnection = new SqlConnection(sourceConnectionString);
                SqlConnection destinationConnection = new SqlConnection(destinationConnectionString);

                try
                {
                    sourceConnection.Open();
                    destinationConnection.Open();

                    string selectQuery = "SELECT * FROM " + sourceTable;
                    SqlCommand selectCommand = new SqlCommand(selectQuery, sourceConnection);
                    SqlDataReader reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        // Sync each record to destination table
                        string insertQuery = "INSERT INTO " + destinationTable + " (Column1, Column2, Column3) VALUES (@Column1, @Column2, @Column3)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, destinationConnection);
                        insertCommand.Parameters.AddWithValue("@Column1", reader["Column1"]);
                        insertCommand.Parameters.AddWithValue("@Column2", reader["Column2"]);
                        insertCommand.Parameters.AddWithValue("@Column3", reader["Column3"]);
                        insertCommand.ExecuteNonQuery();
                    }

                    Console.WriteLine("Synced records from " + sourceTable + " to " + destinationTable + ".");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error syncing records: " + ex.Message);
                }
                finally
                {
                    sourceConnection.Close();
                    destinationConnection.Close();
                }
            }
            else if (args[0] == "replace")
            {
                // Open a text file and replace a particular string
                if (args.Length != 2)
                {
                    Console.WriteLine("Usage: program.exe replace [filename]");
                    return;
                }

                string filename = args[1];
                string tempFilename = Path.GetTempFileName();

                try
                {
                    using (StreamReader reader = new StreamReader(filename))
                    using (StreamWriter writer = new StreamWriter(tempFilename))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            line = line.Replace("oldstring", "newstring");
                            writer.WriteLine(line);
                        }
                    }

                    File.Delete(filename);
                    File.Move(tempFilename, filename);

                    Console.WriteLine("Replaced string in file " + filename + ".");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error replacing string in file: " + ex.Message);
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
===
