using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Abstract.Repositories;
using TodoApp.Application.Dto;
using TodoApp.Domain.Todos;

namespace TodoApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepo;

        public TodosController(ITodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await _todoRepo.GetAllTodos();
            if (!todos.Any())
            {
                return NoContent();
            }
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(Guid id)
        {
            var todo = await _todoRepo.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var todo = await _todoRepo.CreateTodo(model);
            return Ok(todo);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTodo([FromBody] Todo model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var todo = await _todoRepo.UpdateTodo(model);
            if (todo == null)
            {
                return BadRequest();
            }
            return Ok(todo);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var res = await _todoRepo.DeleteTodo(id);
            if (!res)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
