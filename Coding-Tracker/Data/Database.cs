using Coding_Tracker.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Coding_Tracker.Data
{
    internal class Database
    {
        internal static string ConnectionString { get; } = "Data Source=coding_sessions.db;";

        internal static void CreateDatabase()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {

                // Ensure the table exists
                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS CodingSessions (
                Id INTEGER PRIMARY KEY,
                ProjectName TEXT NOT NULL,
                StartTime TEXT NOT NULL,
                EndTime TEXT NOT NULL,
                Duration TEXT NOT NULL
                )";

                connection.Execute(createTableQuery);
            }
        }

        List<CodingSession> codingSessions = new List<CodingSession>();

    }
}
