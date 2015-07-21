
CREATE PROCEDURE [Report].[spCreateContainerViewEntry]
	@Name					VARCHAR(128),
	@ViewID					INT=NULL,
	@ParentContainerViewID	INT=NULL,
	@ContainerViewID		INT OUTPUT
AS
SET NOCOUNT ON
BEGIN
	DECLARE @ContainerID INT

	BEGIN TRANSACTION

		BEGIN TRY
			SELECT	@ContainerID = ContainerID
			FROM	Content.Container
			WHERE	Name = @Name

			IF ISNULL(@ContainerID, 0) = 0
			BEGIN

				INSERT INTO Content.Container(Name)
				VALUES(@Name)

				SET @ContainerID =SCOPE_IDENTITY()

			END

			IF ISNULL(@ViewID, 0) != 0
			BEGIN
				
				INSERT INTO Content.ContainerViews(ViewID, ContainerID, ParentContainerViewID)
				VALUES(@ViewID, @ContainerID, @ParentContainerViewID)

				SET @ContainerViewID =SCOPE_IDENTITY()
			END

		END TRY

		BEGIN CATCH

			IF @@TRANCOUNT > 0
			BEGIN
				ROLLBACK TRANSACTION
			END;

			THROW;

		END CATCH

	IF @@TRANCOUNT > 0
	BEGIN
		COMMIT TRANSACTION
	END

	RETURN @ContainerViewID

END