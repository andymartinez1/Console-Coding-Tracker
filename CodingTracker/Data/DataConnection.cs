using Coding_Tracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Coding_Tracker.Data;

public class DataConnection
{
    private readonly string _connectionString;

    private readonly IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    public DataConnection()
    {
        _connectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
    }

    internal void CreateDatabase()
    {
        using (var connection = new SqliteConnection(_connectionString))
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
            SeedSessions(10);
    }

    internal void InsertSession(CodingSession session)
    {
        using (var connection = new SqliteConnection(_connectionString))
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
        using (var connection = new SqliteConnection(_connectionString))
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
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var selectQuery = "SELECT * FROM CodingSessions";

            var sessions = connection.Query<CodingSession>(selectQuery).ToList();

            return sessions;
        }
    }

    internal void UpdateSession(CodingSession session)
    {
        using (var connection = new SqliteConnection(_connectionString))
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
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var deleteQuery = "DELETE FROM CodingSessions WHERE Id = @Id";

            connection.Execute(deleteQuery, new { Id = id });
        }
    }

    internal void SeedSessions(int count)
    {
        var random = new Random();
        var currentDate = DateTime.Now.Date;

        var sessions = new List<CodingSession>();

        for (var i = 0; i < count; i++)
        {
            var startTime = currentDate.AddHours(random.Next(0, 12)).AddMinutes(random.Next(0, 60));
            var endTime = startTime.AddHours(random.Next(1, 12)).AddMinutes(random.Next(0, 60));

            var session = new CodingSession
            {
                ProjectName = $"Project {i + 1}",
                StartTime = startTime,
                EndTime = endTime,
            };

            sessions.Add(session);
            currentDate = currentDate.AddDays(1);
        }

        InsertSeedSessions(sessions);
    }

    internal bool IsTableEmpty()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM CodingSessions");

            return count == 0;
        }
    }
}
