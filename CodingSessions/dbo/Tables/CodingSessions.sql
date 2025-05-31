CREATE TABLE [dbo].[CodingSessions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	ProjectName TEXT NOT NULL,
    StartTime TEXT NOT NULL,
    EndTime TEXT NOT NULL,
    Duration FLOAT NOT NULL
)
