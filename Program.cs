using System;
using Microsoft.Data.Sqlite;

namespace habitLogger {

    class Program {

        static void Main(string[] args) {
            string connectionString = @"Data Source=habitLogger.db";

            using (var connection = new SqliteConnection(connectionString)) {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = 
                    @"CREATE TABLE IF NOT EXISTS drinking_water (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Quantity INTEGER
                    )";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }

            static void GetUserInput() {
                Console.Clear();
                bool closeApp = false;
                while (closeApp == false) {
                    Console.WriteLine("\n\nMAINU MENU");
                    Console.WriteLine("\nWhat would you like to do?");
                    Console.WriteLine("\nType 0 to Close Application");
                    Console.WriteLine("Type 1 to View All Records");
                    Console.WriteLine("Type 2 to Insert Record");
                    Console.WriteLine("Type 3 to Delete Record");
                    Console.WriteLine("Type 4 to Update Record");
                    Console.WriteLine("-----------------------------------------\n");


                }
            }
        }
    }
}