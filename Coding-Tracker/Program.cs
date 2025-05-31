using Coding_Tracker.Data;
using Coding_Tracker.Models;

Database.CreateDatabase();

CodingSession codingSession = new CodingSession(
    1,
    "Coding Tracker",
    DateTime.Now.AddHours(-2),
    DateTime.Now,
    2.0
);

string insertQuery = @"INSERT INTO CodingSessions (Id, ProjectName, StartTime, EndTime, Duration) 
                       VALUES (@Id, @ProjectName, @StartTime, @EndTime, @Duration)";


//connection.Query(insertQuery, codingSession);