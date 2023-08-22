# ToDoListAPI

Database

    CREATE DATABASE ToDo
    USE ToDo
    --CREATE TABLE FOR ITEM, WHICH SHOULD ADD IN ToDoList
    CREATE TABLE ToDoItem
    (
    Id uniqueidentifier	PRIMARY KEY DEFAULT NEWID(),
    Title NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(max),
    Completed BIT NOT NULL DEFAULT 0
    );
    insert into ToDoItem(Id, Title, [Description], Completed)
    values
    (NEWID(), 'Do Task5', 'do my task in .net5', 0),
    (NEWID(), 'Do Task6', 'do my task in .net6', 1)
    
    select * from ToDoItem
    
    --CREATE STORED PROCEDURES
    --GET ALL LIST
    CREATE PROCEDURE GetAll
    AS
    BEGIN
    	SELECT * FROM ToDoItem
    END
    --GET COMPLITED ToDos
    CREATE PROCEDURE GetComplited
    AS
    BEGIN
    	SELECT * FROM ToDoItem
    	WHERE Completed=1
    END
    --GET UNCOMPLITED ToDos
    CREATE PROCEDURE GetUncomplited
    AS
    BEGIN
    	SELECT * FROM ToDoItem
    	WHERE Completed=0
    END
    --MAKE ToDo DONE
    CREATE PROCEDURE DoneToDo
        @TodoId uniqueidentifier
    AS
    BEGIN
        DECLARE @IsCompleted bit;

    -- Check if the ToDo item exists
    IF EXISTS (SELECT 1 FROM ToDoItem WHERE Id = @TodoId)
    BEGIN
        -- Check if the ToDo item is already completed
        SELECT @IsCompleted = Completed FROM ToDoItem WHERE Id = @TodoId;

        IF @IsCompleted = 0
        BEGIN
            -- Update the ToDo item as completed
            UPDATE ToDoItem
            SET Completed = 1
            WHERE Id = @TodoId;

            SELECT 'ToDo item marked as completed successfully.' AS Message;
        END
        ELSE
        BEGIN
            SELECT 'This ToDo item is already completed.' AS Message;
        END
    END
    ELSE
    BEGIN
        SELECT 'ToDo item not found.' AS Message;
    END
    END
    
    --ADD NEW ToDo
    ALTER PROCEDURE AddToDo
    	@title NVARCHAR(100),
    	@description NVARCHAR(max)
    AS
    BEGIN
    	-- Insert the new ToDo item
    	INSERT INTO ToDoItem (Id, Title, Description, Completed)
    	VALUES (NEWID(), @title, @description, 0)

	-- Select the last added ToDo item
	SELECT * FROM ToDoItem
	WHERE Title=@title
    END
    --UPDATE ToDo
    CREATE PROCEDURE UpdateToDo
    	@todoId uniqueidentifier,
    	@title NVARCHAR(100),
    	@description NVARCHAR(max),
    	@completed BIT
    AS 
    BEGIN
    	IF EXISTS (SELECT 1 FROM ToDoItem WHERE Id=@todoId)
    	BEGIN
    		--UPDATE ITEM
    		UPDATE ToDoItem
    		SET Title=@title,
    			Description=@description,
    			Completed=@completed
    		WHERE Id=@todoId

		SELECT 'ToDo item updated successfully.' AS Message;

	END
	ELSE
	BEGIN
		SELECT 'ToDo item not found.' AS Message;
	END
    END
    
    --DLEETE ToDo WITH CERTAIN ID
    CREATE PROCEDURE DeleteToDo
    	@todoId uniqueidentifier
    AS
    BEGIN
    	IF EXISTS (SELECT 1 FROM ToDoItem WHERE Id=@todoId)
    	BEGIN
    		DELETE FROM ToDoItem
    		WHERE Id=@todoId
    		SELECT 'ToDo item deleted successfully.' AS Message;
    	END
    	ELSE
    	BEGIN
    		SELECT 'ToDo item not found.' AS Message;
    	END
    END
