CREATE PROCEDURE [dbo].[spCodingSessions_Delete]
	@Id INT
AS
begin
	DELETE
	FROM dbo.[CodingSessions]
	WHERE Id = @Id; 
end
