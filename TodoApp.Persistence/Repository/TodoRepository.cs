using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Abstract.Persistence;
using TodoApp.Application.Abstract.Repositories;
using TodoApp.Application.Dto;

namespace TodoApp.Persistence.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public TodoRepository(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetTodoDto> CreateTodo(CreateTodoDto model)
        {
            var todo = _mapper.Map<Domain.Todos.Todo>(model);
            _context.Todos.Add(todo);
            var res = await _context.SaveChangesAsync(CancellationToken.None);
            if (res > 0)
            {
                return _mapper.Map<GetTodoDto>(todo);
            }
            return null;
        }

        public async Task<bool> DeleteTodo(Guid id)
        {
            var todo = await _context.Todos.SingleOrDefaultAsync(x => x.Id.Equals(id));
            _context.Todos.Remove(todo);
            return await _context.SaveChangesAsync(CancellationToken.None) > 0;
        }

        public async Task<IEnumerable<GetTodoDto>> GetAllTodos()
        {
            var todos = await _context.Todos.ToListAsync();
            return _mapper.Map<IEnumerable<GetTodoDto>>(todos);
        }

        public async Task<GetTodoDto> GetTodoById(Guid id)
        {
            var todo = await _context.Todos.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return _mapper.Map<GetTodoDto>(todo);
        }

        public async Task<IEnumerable<GetTodoDto>> GetTodosByStatus(bool isDone = true)
        {
            var todos = await _context.Todos.Where(x => x.IsDone.Equals(isDone)).ToListAsync();
            return _mapper.Map<IEnumerable<GetTodoDto>>(todos);
        }

        public async Task<GetTodoDto> UpdateTodo(UpdateTodoDto model)
        {
            var todoInDb = await _context.Todos.SingleOrDefaultAsync(x => x.Id.Equals(model.Id));
            var todo = _mapper.Map(model, todoInDb);
            _context.Todos.Attach(todo);
            var res = await _context.SaveChangesAsync(CancellationToken.None);
            if (res > 0)
            {
                return _mapper.Map<GetTodoDto>(todo);
            }
            return null;

        }
    }
}
