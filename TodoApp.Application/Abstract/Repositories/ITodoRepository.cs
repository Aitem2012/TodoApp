using TodoApp.Application.Dto;
using TodoApp.Domain.Todos;

namespace TodoApp.Application.Abstract.Repositories
{
    public interface ITodoRepository
    {
        Task<Todo> CreateTodo(CreateTodoDto model);
        Task<Todo> UpdateTodo(Todo model);
        Task<IEnumerable<Todo>> GetAllTodos();
        Task<bool> DeleteTodo(Guid id);
        Task<Todo> GetTodoById(Guid id);
    }
}
