using Microsoft.Data.Sqlite;

namespace CheckMyFlightApi.Infrastructure;

public class DatabaseSeeder : IDatabaseSeeder
{
    //We're not going to have files with db on GitHub
    //All files going to be created in bin/debug
    private readonly string dbPath = Path.Combine(AppContext.BaseDirectory, "database.db");


    /// <summary>
    /// Seeds the database with necessary tables and initial data if they do not already exist.
    /// </summary>
    public void SeedDatabase()
    {
        using (var db = new SqliteConnection($"Data Source={dbPath}"))
        {
            db.Open();
            
            var createCommand =  new SqliteCommand();
            createCommand.Connection = db;

            createCommand.CommandText =
                "CREATE TABLE Flights (Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "FlightNumber TEXT NOT NULL," +
                "DeparturePlace TEXT," +
                "ArrivalPlace TEXT," +
                "CanGetReturnMoney TEXT)";

            createCommand.ExecuteReader();
            
            Console.WriteLine("Database seeded successfully!");
        }
    }
}