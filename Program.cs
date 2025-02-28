﻿using System;
using System.ComponentModel;
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
                        Environment.Exit(0);
                        break;
                    case "1":
                        GetAllRecords();
                        break;
                    case "2":
                        Insert();
                        break;
                    case "3":
                        Delete();
                        break;
                    case "4":
                        Update();
                        break;
                }


            }
        }

        internal static void Update() {
            Console.Clear();
            GetAllRecords();

            var recordId = GetNumberInput("\n\nPlease type Id of the record you want to update.");

            using (var connection = new SqliteConnection(connectionString)) {

                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM visiting_gym WHERE Id = {recordId})";
                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (checkQuery == 0) {
                    Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist\n\n");
                    connection.Close();
                    Update();
                }

                string date = GetDateInput();

                int quantity = GetNumberInput("\n\nPlease enter number times visited");

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"UPDATE visiting_gym SET date = '{date}', quantity = {quantity} WHERE id = {recordId}";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            Console.WriteLine("Updated");

        

        }


        private static void Delete()
        {

            // string date = GetDateInput();
            Console.Clear();
            GetAllRecords();

            var recordId = GetNumberInput("\n\n Please type the Id of the record you want to delete\n\n");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    // $"DELETE FROM visiting_gym WHERE date = '{date}'";
                    $"DELETE FROM visiting_gym WHERE Id = '{recordId}'";

                int rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist. \n\n");
                    Delete();
                }
                connection.Close();
            }

            Console.WriteLine($"\n\nRecord with Id {recordId} was deleted \n\n");
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

        private static void GetAllRecords()
        {
            Console.Clear();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                $"SELECT * FROM visiting_gym";

                List<GymTimes> tableData = new();

                SqliteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new GymTimes
                            {
                                Id = reader.GetInt32(0),
                                Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),
                                Quantity = reader.GetInt32(2)

                            }
                        );
                    }
                }
                else
                {
                    Console.WriteLine("No rows found");
                }

                connection.Close();
                Console.WriteLine("--------------------------\n");
                foreach (var gt in tableData)
                {
                    Console.WriteLine($"{gt.Id} - {gt.Date.ToString("dd-MMM-yyyy")} - {gt.Quantity} times");
                }
                Console.WriteLine("-------------------------------\n");
            }
        }

        internal static string GetDateInput()
        {
           

            string dateInput = Console.ReadLine();

            if (dateInput == "0") GetUserInput();

            while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _)) {
                Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to main menu");
                dateInput = Console.ReadLine();
            }

            return dateInput;

        }

        internal static int GetNumberInput(string message)
        {
            // Add validation for negative non/nmbers
            Console.WriteLine(message);

            string numberInput = Console.ReadLine();

            if (numberInput == "0") GetUserInput();

            while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0) {
                Console.WriteLine("\n\nInvalid number. Try again \n\n");
                numberInput = Console.ReadLine();
            }

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

