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

        public async Task<IEnumerable<ToDo>> GetAllTodos()
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

        public async Task<IEnumerable<ToDo>> GetCompletedToDos()
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

        public async Task<IEnumerable<ToDo>> GetUncompletedToDos()
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
        public async Task<string> MakeTodoDone([FromForm] Guid todoId)
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
        public async Task<ToDo> AddToDo(ToDoDTO todo)
        {
            throw new NotImplementedException();
        }




        public async Task<IActionResult> UpdateTodo(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> DeleteTodo(Guid guid)
        {
            throw new NotImplementedException();
        }

    }
}
