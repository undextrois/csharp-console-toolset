using System;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.IO;

namespace CooperConsoleApp
{
    class Cooper
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter an option: process or help");
            string option = Console.ReadLine();
            switch (option)
            {
                case "process":
                    Console.WriteLine("Please choose a database: mysql or mssql");
                    string dbOption = Console.ReadLine();
                    string connectionString;
                    if (dbOption == "mysql")
                    {
                        connectionString = "server=localhost;user=root;password=password;database=mydb;";
                    }
                    else if (dbOption == "mssql")
                    {
                        connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb;Integrated Security=True;";
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please choose mysql or mssql.");
                        return;
                    }
                    Console.WriteLine("Connecting to the database...");
                    Spinner spinner = new Spinner();
                    spinner.Start();
                    Thread.Sleep(3000);
                    spinner.Stop();
                    try
                    {
                        using (var connection = new MySqlConnection(connectionString)) // use SqlConnection for MSSQL
                        {
                            connection.Open();
                            Console.WriteLine("Connected to the database successfully.");
                            Console.WriteLine("Please enter the file path:");
                            string filePath = Console.ReadLine();
                            string[] lines = File.ReadAllLines(filePath);
                            int totalLines = lines.Length;
                            int insertedLines = 0;
                            Console.WriteLine("Inserting data into the database...");
                            Countdown countdown = new Countdown(totalLines);
                            countdown.Start();
                            foreach (string line in lines)
                            {
                                // extract data from line and insert into database
                                insertedLines++;
                                countdown.Update(insertedLines);
                            }
                            countdown.Stop();
                            Console.WriteLine("Finished inserting data into the database.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                case "help":
                    Console.WriteLine("Help information goes here.");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose process or help.");
                    break;
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    class Spinner
    {
        private bool _active;
        private Thread _thread;

        public void Start()
        {
            _active = true;
            _thread = new Thread(Animate);
            _thread.Start();
        }

        public void Stop()
        {
            _active = false;
            Thread.Sleep(100); // give time for animation to stop
        }

        private void Animate()
        {
            int counter = 0;
            while (_active)
            {
                Console.Write("|");
                Thread.Sleep(100);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write("/");
                Thread.Sleep(100);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write("-");
                Thread.Sleep(100);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write("\\");
                Thread.Sleep(100);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                counter++;
            }
        }
    }

class Countdown
{
    private int _total;
    private int _current;
    private Thread _thread;

    public Countdown(int total)
    {
        _total = total;
        _current = 0;
        _thread = new Thread(Animate);
    }

    public void Start()
    {
        _thread.Start();
    }

    public void Stop()
    {
        Console.WriteLine();
    }

    public void Update(int current)
    {
        _current = current;
    }

    private void Animate()
    {
        Console.Write("Progress: [");
        while (_current < _total)
        {
            int progress = (_current * 100) / _total;
            Console.Write(progress + "%");
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(">");
            Thread.Sleep(100);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write("-");
            _current++;
        }
        Console.Write("100%]\n");
    }
}

       
