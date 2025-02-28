using System;
using System.Globalization;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.FileIO;

namespace habitLogger
{

    class Program
    {
        static string connectionString = @"Data Source=habitLogger.db";

        static void Main(string[] args)
        {

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS visiting_gym (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Quantity INTEGER
                    )";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }

            GetUserInput();

        }
        static void GetUserInput()
        {
            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application");
                Console.WriteLine("Type 1 to View All Records");
                Console.WriteLine("Type 2 to Insert Record");
                Console.WriteLine("Type 3 to Delete Record");
                Console.WriteLine("Type 4 to Update Record");
                Console.WriteLine("-----------------------------------------\n");

                string commandInput = Console.ReadLine();

                switch (commandInput)
                {
                    case "0":
                        Console.WriteLine("\nGoodbye!\n");
                        closeApp = true;
                        break;
                    case "1":
                        // GetAllRecords();
                        break;
                    case "2":
                        Insert();
                        break;
                        // case "3":
                        //     DeleteDi
                }


            }
        }


        
        private static void Insert()
        {
            string date = GetDateInput();

            int quantity = GetNumberInput("\n\n Please insert number of times visited gym\n\n");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $"INSERT INTO visiting_gym(date, quantity) VALUES('{date}', {quantity})";


                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        internal static string GetDateInput()
        {
            Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to main menu");

            string dateInput = Console.ReadLine();

            if (dateInput == "0") GetUserInput();

            return dateInput;

        }

        internal static int GetNumberInput(string message)
        {
            Console.WriteLine(message);

            string numberInput = Console.ReadLine();

            if (numberInput == "0") GetUserInput();

            int finalInput = Convert.ToInt32(numberInput);

            return finalInput;
        }


    }
}

public class GymTimes
{
    public int Id { get; set; }
    public DateTime Date { get; set; }


    public int Quantity { get; set; }
}

