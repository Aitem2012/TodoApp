using TodoApp.Application.Dto;

namespace TodoApp.Application.Abstract.Repositories
{
    public interface ITodoRepository
    {
        Task<GetTodoDto> CreateTodo(CreateTodoDto model);
        Task<GetTodoDto> UpdateTodo(UpdateTodoDto model);
        Task<IEnumerable<GetTodoDto>> GetAllTodos();
        Task<bool> DeleteTodo(Guid id);
        Task<GetTodoDto> GetTodoById(Guid id);
        Task<IEnumerable<GetTodoDto>> GetTodosByStatus(bool isDone = true);
    }
}
