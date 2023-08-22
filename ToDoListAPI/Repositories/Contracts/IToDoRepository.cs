using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Models;
using ToDoListAPI.Models.DTO;

namespace ToDoListAPI.Repositories.Contracts
{
    public interface IToDoRepository
    {
        public Task<IEnumerable<ToDo>> GetAllTodos();
        public Task<IEnumerable<ToDo>> GetUncompletedToDos();
        public Task<IEnumerable<ToDo>> GetCompletedToDos();
        public Task<string> MakeTodoDone(Guid todoId);
        public Task<ToDo> AddToDo(ToDoDTO todo);
        public Task<IActionResult> UpdateTodo(Guid guid);
        
        public Task<IActionResult> DeleteTodo(Guid guid);

    }
}
