using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Abstract.Repositories;
using TodoApp.Application.Dto;
using TodoApp.Persistence.Context;

namespace TodoApp.Persistence.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;


        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Todos.Todo> CreateTodo(CreateTodoDto model)
        {
            var todo = new Domain.Todos.Todo
            {
                Id = Guid.NewGuid(),
                IsDone = model.IsDone,
                Time = model.Time,
                Title = model.Title,
                Description = model.Description
            };
            _context.Todos.Add(todo);
            var res = await _context.SaveChangesAsync(CancellationToken.None);
            if (res > 0)
            {
                return todo;
            }
            return null;
        }

        public async Task<bool> DeleteTodo(Guid id)
        {
            var todo = await _context.Todos.SingleOrDefaultAsync(x => x.Id.Equals(id));
            if (todo == null)
            {
                return false;
            }
            _context.Todos.Remove(todo);
            return await _context.SaveChangesAsync(CancellationToken.None) > 0;
        }

        public async Task<IEnumerable<Domain.Todos.Todo>> GetAllTodos()
        {
            var todos = await _context.Todos.ToListAsync();
            return todos;
        }

        public async Task<Domain.Todos.Todo> GetTodoById(Guid id)
        {
            var todo = await _context.Todos.SingleOrDefaultAsync(x => x.Id.Equals(id));
            if (todo == null)
            {
                return null;
            }
            return todo;
        }

        public async Task<Domain.Todos.Todo> UpdateTodo(Domain.Todos.Todo model)
        {
            var todoInDb = await _context.Todos.SingleOrDefaultAsync(x => x.Id.Equals(model.Id));
            if (todoInDb == null)
            {
                return null;
            }

            todoInDb.Id = model.Id;
            todoInDb.Title = model.Title;
            todoInDb.Time = model.Time;
            todoInDb.Description = model.Description;
            todoInDb.IsDone = model.IsDone;
            _context.Todos.Attach(todoInDb);
            var res = await _context.SaveChangesAsync(CancellationToken.None);
            if (res > 0)
            {
                return todoInDb;
            }
            return null;

        }
    }
}
