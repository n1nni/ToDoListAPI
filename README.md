# ToDoListAPI using ASP.NET Core and Dapper

This project is a ToDoListAPI built using ASP.NET Core, which provides a set of API endpoints for managing ToDo items. It utilizes Dapper for database interaction and follows RESTful principles. Below is a comprehensive overview of the project, including its components, usage, endpoints, and database structure.

## Components

The project consists of the following main components:

- **Logging Configuration (appsettings.json):** The logging configuration specifies log levels for different categories and includes settings for console logging.
- **Program.cs:** This file contains the main application entry point. It sets up the services and configurations for the ASP.NET Core application.
- **IToDoRepository.cs:** This interface defines the contract for interacting with the ToDo database, including methods for fetching, adding, updating, and deleting ToDo items.
- **ToDoRepository.cs:** This class implements the IToDoRepository interface. It uses Dapper to interact with the database and provides methods to perform CRUD operations on ToDo items.
- **ToDoDTO.cs:** This class defines the Data Transfer Object (DTO) for creating and updating ToDo items. It holds the properties for the title and description of a ToDo item.
- **ToDoController.cs:** This controller class handles incoming API requests and delegates the execution of corresponding repository methods. It includes endpoints for fetching all ToDo items, completed ToDo items, uncompleted ToDo items, marking ToDo items as done, adding new ToDo items, updating ToDo items, and deleting ToDo items.

## Endpoints

The API provides the following endpoints:

- **GET /api/ToDo/get-toDos:** Fetches all ToDo items.
- **GET /api/ToDo/completed:** Fetches completed ToDo items.
- **GET /api/ToDo/uncompleted:** Fetches uncompleted ToDo items.
- **PUT /api/ToDo/mark-done/{todoId}:** Marks a ToDo item as done.
- **POST /api/ToDo/add:** Adds a new ToDo item.
- **PUT /api/ToDo/update/{guid}:** Updates a ToDo item.
- **DELETE /api/ToDo/delete/{guid}:** Deletes a ToDo item.

## Getting Started

To get started with the ToDoListAPI project, follow these steps:

1. **Clone this repository:** Use your preferred method to clone this repository to your local machine.

2. **Open the project:** Open the cloned project in Visual Studio or your preferred integrated development environment (IDE).

3. **Configure the database connection string:** In the `appsettings.json` file, locate the database connection string configuration and set it according to your database setup.

4. **Build and run the project:** Build the project in your IDE and run it. This will start the ToDoListAPI.

5. **Interact with the API:** Use the provided API endpoints to manage ToDo items. You can use tools like [curl](https://curl.se/) or [Postman](https://www.postman.com/) to send requests to the API.

6. **Explore API documentation:** For detailed API documentation, navigate to the Swagger UI by opening your web browser and going to `/swagger` once the application is running. This interactive documentation will help you understand the available endpoints, their inputs, and expected responses.

By following these steps, you'll be able to set up the ToDoListAPI project, interact with its endpoints, and explore its API documentation.


## Database Structure

The database schema includes a single table named ToDoItem, which stores ToDo items. Below is the SQL script that demonstrates how the ToDoItem table is created:


	
	CREATE TABLE ToDoItem (
	    Id UNIQUEIDENTIFIER PRIMARY KEY,
	    Title NVARCHAR(100),
	    Description NVARCHAR(MAX),
	    Completed BIT
	);

Additionally, stored procedures are used to interact with the database. Below are the SQL scripts for the stored procedures:

### GetAllTodos
    CREATE PROCEDURE GetAll
    AS
    BEGIN
    	SELECT * FROM ToDoItem
    END

### GetCompletedTodos

	--GET COMPLITED ToDos
	CREATE PROCEDURE GetComplited
	AS
	BEGIN
	SELECT * FROM ToDoItem
	WHERE Completed=1
	END
 ### GetUncompletedTodos
 
    CREATE PROCEDURE GetUncomplited
    AS
    BEGIN
    	SELECT * FROM ToDoItem
    	WHERE Completed=0
    END
### MarkTodoAsDone

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
### AddTodo

    CREATE PROCEDURE AddToDo
    	@title NVARCHAR(100),
    	@description NVARCHAR(max)
    AS
    BEGIN
    	-- Insert the new ToDo item
    	INSERT INTO ToDoItem (Id, Title, Description, Completed)
    	VALUES (NEWID(), @title, @description, 0)
     
	SELECT * FROM ToDoItem
	WHERE Title=@title
    END
### UpdateTodo

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

### DeleteTodo

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

## Support & Contact
This project was created by [n1nni](https://github.com/n1nni). If you encounter any issues, have suggestions for improvements, or need help with this project, feel free to reach out to me.

Contact Information:

:email: - Email: [nin.dautashvili@gmail.com](mailto:nin.dautashvili@gmail.com)

:email: - LinkedIn: [Nino Dautashvili](https://www.linkedin.com/in/nino-dautashvili/)

Thank you for checking out ToDoAPI project! :rocket:
