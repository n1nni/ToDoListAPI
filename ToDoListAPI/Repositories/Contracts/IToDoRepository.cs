using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Models;
using ToDoListAPI.Models.DTO;

namespace ToDoListAPI.Repositories.Contracts
{
    public interface IToDoRepository
    {
        public Task<IEnumerable<ToDo>> GetAllTodosAsync();
        public Task<IEnumerable<ToDo>> GetUncompletedToDosAsync();
        public Task<IEnumerable<ToDo>> GetCompletedToDosAsync();
        public Task<string> MakeTodoDoneAsync(Guid todoId);
        public Task<IEnumerable<ToDo>> AddToDoAsync(ToDoDTO todo);
        public Task<string> UpdateTodoAsync(Guid guid, string title, string description, bool completed);
        public Task<string> DeleteTodoAsync(Guid guid);

    }
}
