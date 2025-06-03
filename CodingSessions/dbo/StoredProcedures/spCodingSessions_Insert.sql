CREATE PROCEDURE [dbo].[spCodingSessions_Insert]
	@ProjectName TEXT,
	@StartTime TEXT,
	@EndTime TEXT,
	@Duration FLOAT
AS
begin
	INSERT INTO dbo.[CodingSessions] (ProjectName, StartTime, EndTime, Duration)
	VALUES (@ProjectName, @StartTime, @EndTime, @Duration);
end
