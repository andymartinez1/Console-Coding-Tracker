using Coding_Tracker.Controllers;
using Coding_Tracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Coding_Tracker.Data;

internal class DataConnection
{
    private static string ConnectionString;

    private readonly IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    public DataConnection()
    {
        ConnectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
    }

    internal void CreateDatabase()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            // Ensure the table exists
            var createTableQuery =
                @"
                CREATE TABLE IF NOT EXISTS CodingSessions (
                Id INTEGER PRIMARY KEY,
                ProjectName TEXT NOT NULL,
                StartTime TEXT NOT NULL,
                EndTime TEXT NOT NULL
                )";

            connection.Execute(createTableQuery);
        }

        // Seed the database with initial data if it's empty
        var isEmpty = IsTableEmpty();
        if (isEmpty)
            CodingController.SeedSessions(10);
    }

    internal void InsertSession(CodingSession session)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var insertQuery =
                @"
                    INSERT INTO CodingSessions (ProjectName, StartTime, EndTime)
                    VALUES (@ProjectName, @StartTime, @EndTime)";

            connection.Execute(
                insertQuery,
                new
                {
                    session.ProjectName,
                    session.StartTime,
                    session.EndTime,
                }
            );
        }
    }

    internal void InsertSeedSessions(List<CodingSession> sessions)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var insertQuery =
                @"
                    INSERT INTO CodingSessions (ProjectName, StartTime, EndTime)
                    VALUES (@ProjectName, @StartTime, @EndTime)";

            foreach (var session in sessions)
                connection.Execute(
                    insertQuery,
                    new
                    {
                        session.ProjectName,
                        session.StartTime,
                        session.EndTime,
                    }
                );
        }
    }

    internal List<CodingSession> GetAllSessions()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var selectQuery = "SELECT * FROM CodingSessions";

            var sessions = connection.Query<CodingSession>(selectQuery).ToList();

            return sessions;
        }
    }

    internal void UpdateSession(CodingSession session)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var updateQuery =
                @"
                    UPDATE CodingSessions
                    SET ProjectName = @ProjectName, StartTime = @StartTime, EndTime = @EndTime
                    WHERE Id = @Id";

            connection.Execute(
                updateQuery,
                new
                {
                    session.ProjectName,
                    session.StartTime,
                    session.EndTime,
                    session.Id,
                }
            );
        }
    }

    internal void DeleteSession(int id)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var deleteQuery = "DELETE FROM CodingSessions WHERE Id = @Id";

            connection.Execute(deleteQuery, new { Id = id });
        }
    }

    internal static bool IsTableEmpty()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM CodingSessions");

            return count == 0;
        }
    }
}
