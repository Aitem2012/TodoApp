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
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(Guid id)
        {
            var todo = await _todoRepo.GetTodoById(id);
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
            return Ok(todo);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var res = await _todoRepo.DeleteTodo(id);
            if (!res)
            {
                return BadRequest("Todo could not be deleted");
            }
            return Ok(res);
        }
    }
}
