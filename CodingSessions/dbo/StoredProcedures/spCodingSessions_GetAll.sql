CREATE PROCEDURE [dbo].[spCodingSessions_GetAll]
AS
begin
	SELECT *
	FROM dbo.[CodingSessions];
end