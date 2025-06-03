CREATE PROCEDURE [dbo].[spCodingSessions_GetById]
	@Id INT
AS
begin
	SELECT Id, ProjectName, StartTime, EndTime, Duration
	FROM dbo.[CodingSessions]
	WHERE Id = @Id; 
end
