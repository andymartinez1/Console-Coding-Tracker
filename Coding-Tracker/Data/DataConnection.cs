using Coding_Tracker.Models;
using Microsoft.Data.Sqlite;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Coding_Tracker.Data;

internal class DataConnection
{

    IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

    private static string ConnectionString;

    public DataConnection()
    {
        ConnectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
    }

    internal static void CreateDatabase()
    {

        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            // Ensure the table exists
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS CodingSessions (
                Id INTEGER PRIMARY KEY,
                ProjectName TEXT NOT NULL,
                StartTime TEXT NOT NULL,
                EndTime TEXT NOT NULL
                )";

            connection.Execute(createTableQuery);
        }
    }

    internal void InsertSession(CodingSession session)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            string insertQuery = @"
                    INSERT INTO CodingSessions (ProjectName, StartTime, EndTime)
                    VALUES (@ProjectName, @StartTime, @EndTime)";

            connection.Execute(insertQuery, new { session.ProjectName, session.StartTime, session.EndTime });

        }
    }

    internal List<CodingSession> GetSessions()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            string selectQuery = "SELECT * FROM CodingSessions";

            var sessions = connection.Query<CodingSession>(selectQuery).ToList();

            foreach (var session in sessions)
            {
                session.Duration = session.EndTime - session.StartTime;
            }

            return sessions;
        }
    }
}
