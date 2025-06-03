CREATE PROCEDURE [dbo].[spCodingSessions_Update]
	@Id INT,
	@ProjectName TEXT,
	@StartTime TEXT,
	@EndTime TEXT,
	@Duration FLOAT
AS
begin
	UPDATE dbo.[CodingSessions] 
	SET 
		ProjectName = @ProjectName, 
		StartTime = @StartTime, 
		EndTime = @EndTime, 
		Duration = @Duration
	WHERE Id = Id;
end
