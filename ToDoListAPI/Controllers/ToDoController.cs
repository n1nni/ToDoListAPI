using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ToDoListAPI.Models;
using ToDoListAPI.Models.DTO;
using ToDoListAPI.Repositories.Contracts;

namespace ToDoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly ILogger<ToDoController> _logger;

        public ToDoController(IToDoRepository toDoRepository, ILogger<ToDoController> logger)
        {
            _toDoRepository = toDoRepository;
            _logger = logger;
        }

        [HttpGet("get-toDos")]
        public async Task<IActionResult> GetAllTodosAsync()
        {
            try
            {
                var todos = await _toDoRepository.GetAllTodosAsync();
                _logger.LogInformation("Retrieved all ToDo items.");
                return Ok(todos);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while fetching ToDo items.");
                return BadRequest($"An error occurred while fetching ToDo items.\n{ex.Message}");
            }
        }

        [HttpGet("completed")]
        public async Task<IActionResult> GetCompletedToDosAsync()
        {
            try
            {
                var getCompleted = await _toDoRepository.GetCompletedToDosAsync();
                _logger.LogInformation("Retrieved completed ToDo items.");
                return Ok(getCompleted);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while fetching completed ToDo items.");
                return BadRequest($"An error occurred while fetching ToDo items.\n{ex.Message}");
            }
        }

        [HttpGet("uncompleted")]
        public async Task<IActionResult> GetUncompletedToDosAsync()
        {
            try
            {
                var uncompletedToDos = await _toDoRepository.GetUncompletedToDosAsync();
                _logger.LogInformation("Retrieved uncompleted ToDo items.");
                return Ok(uncompletedToDos);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while fetching uncompleted ToDo items.");
                return BadRequest($"An error occurred while fetching ToDo items.\n{ex.Message}");
            }
        }

        [HttpPut("mark-done/{todoId}")]
        public async Task<IActionResult> MarkTodoDoneAsync(Guid todoId)
        {
            try
            {
                var resultString = await _toDoRepository.MakeTodoDoneAsync(todoId);
                _logger.LogInformation(resultString);
                return Ok(resultString);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while using this API.");
                return BadRequest($"An error occurred while using this API.\n{ex.Message}");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToDoAsync(ToDoDTO newToDo)
        {
            try
            {
                var newAdded = await _toDoRepository.AddToDoAsync(newToDo);
                _logger.LogInformation("Added a new ToDo item.");
                return Ok(newAdded);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new ToDo item.");
                return BadRequest($"An error occurred while adding a new ToDo item.\n{ex.Message}");
            }
        }

        [HttpPut("update/{guid}")]
        public async Task<IActionResult> UpdateTodoAsync(Guid guid, string title, string description, bool completed)
        {
            try
            {
                var updateToDo = await _toDoRepository.UpdateTodoAsync(guid, title, description, completed);
                _logger.LogInformation(updateToDo);
                return Ok(updateToDo);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while updating a ToDo item.");
                return BadRequest($"An error occurred while updating a ToDo item.\n{ex.Message}");
            }
        }

        [HttpDelete("delete/{guid}")]
        public async Task<IActionResult> DeleteTodoAsync(Guid guid)
        {
            try
            {
                var result = await _toDoRepository.DeleteTodoAsync(guid);
                _logger.LogInformation(result);
                return Ok(result);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a ToDo item.");
                return BadRequest($"An error occurred while deleting a ToDo item.\n{ex.Message}");
            }
        }
    }
}
