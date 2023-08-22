using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Repositories.Contracts;

namespace ToDoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : Controller
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoController(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        [HttpGet("get-toDos")]
        public async Task<IActionResult> GetAllTodos()
        {
            try
            {
                var todos = await _toDoRepository.GetAllTodos();
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while fetching ToDo items.\n{ex.Message}");
            }
            
            
        }

        [HttpGet("get-completed")]
        public async Task<IActionResult> GetCompletedToDos()
        {
            try
            {
                var getCompleted = await _toDoRepository.GetCompletedToDos();
                return Ok(getCompleted);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while fetching ToDo items.\n{ex.Message}");
            }
        }

        [HttpGet("get-unCompleted")]
        public async Task<IActionResult> GetUncompletedToDos()
        {
            try
            {
                var uncompletedToDos = await _toDoRepository.GetUncompletedToDos();
                return Ok(uncompletedToDos);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while fetching ToDo items.\n{ex.Message}");
            }
        }
        [HttpPut("make-task-done")]
        public async Task<IActionResult> MakeTodoDone([FromForm] Guid todoId)
        {
            try
            {
                var resultString = await _toDoRepository.MakeTodoDone(todoId);
                return Ok(resultString);
            }
            catch(Exception ex)
            {
                return BadRequest($"An error occured while using this API\n{ex.Message}");
            }
        }

        //public Task<IActionResult> AddToDo();
        //public Task<IActionResult> UpdateTodo(Guid guid);
        
        //public Task<IActionResult> DeleteTodo(Guid guid);


    }
}
