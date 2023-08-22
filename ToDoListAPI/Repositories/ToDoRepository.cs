using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.SqlClient;
using ToDoListAPI.Models;
using ToDoListAPI.Models.DTO;
using ToDoListAPI.Repositories.Contracts;

namespace ToDoListAPI.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ToDoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ToDo>> GetAllTodosAsync()
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<ToDo>(
                    sql: "GetAll",
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
        }

        public async Task<IEnumerable<ToDo>> GetCompletedToDosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<ToDo>(
                    sql: "GetComplited",
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
        }

        public async Task<IEnumerable<ToDo>> GetUncompletedToDosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<ToDo>(
                    sql: "GetUncomplited",
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
        }
        public async Task<string> MakeTodoDoneAsync([FromForm] Guid todoId)
        {
            using (var connection =new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@ToDoId", todoId);

                var result = await connection.QueryFirstOrDefaultAsync<string>(
                    sql: "DoneToDo",
                    param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return result;
            }
        }
        public async Task<IEnumerable<ToDo>> AddToDoAsync(ToDoDTO todo)
        {
            using(var connection=new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@title", todo.Title);
                parameters.Add("@description", todo.Description);

                var result = await connection.QueryAsync<ToDo>(
                        sql: "AddToDo",
                        param: parameters,
                        commandType: System.Data.CommandType.StoredProcedure
                    );
                return result;

            }
        }




        public async Task<string> UpdateTodoAsync(Guid guid, string title, string description, bool completed)
        {
            using(var connection=new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@todoId", guid);
                parameters.Add("@title",title);
                parameters.Add("@description",description);
                parameters.Add("@completed",completed);

                var result = await connection.QueryFirstOrDefaultAsync<string>(
                    sql: "UpdateToDo",
                    param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return result;
            }
        }

        public async Task<string> DeleteTodoAsync(Guid guid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@todoId", guid);

                var result = await connection.QueryFirstOrDefaultAsync<string>(

                    sql: "DeleteToDo",
                    param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return result;
            }
        }

    }
}
