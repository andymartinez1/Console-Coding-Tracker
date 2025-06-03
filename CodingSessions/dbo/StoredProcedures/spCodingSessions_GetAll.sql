CREATE PROCEDURE [dbo].[spCodingSessions_GetAll]
AS
begin
	SELECT Id, ProjectName, StartTime, EndTime, Duration
	FROM dbo.[CodingSessions];
end