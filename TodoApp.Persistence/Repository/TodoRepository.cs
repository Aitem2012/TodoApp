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


        public TodoRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<GetTodoDto> CreateTodo(CreateTodoDto model)
        {
            var todo = new Domain.Todos.Todo
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Time = model.Time,
                Description = model.Description,
                IsDone = model.IsDone
            };
            todo.DateCreated = DateTime.Now;
            _context.Todos.Add(todo);
            var res = await _context.SaveChangesAsync(CancellationToken.None);
            if (res > 0)
            {
                return new GetTodoDto()
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Time = todo.Time,
                    IsDone = todo.IsDone
                };
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

        public async Task<IEnumerable<GetTodoDto>> GetAllTodos()
        {
            var todos = await _context.Todos.ToListAsync();
            var todosToReturn = new List<GetTodoDto>();
            foreach (var todo in todos)
            {
                todosToReturn.Add(new GetTodoDto
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Time = todo.Time,
                    IsDone = todo.IsDone
                });
            }
            return todosToReturn;
        }

        public async Task<GetTodoDto> GetTodoById(Guid id)
        {
            var todo = await _context.Todos.SingleOrDefaultAsync(x => x.Id.Equals(id));
            if (todo == null)
            {
                return null;
            }
            return new GetTodoDto()
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Time = todo.Time,
                IsDone = todo.IsDone
            };
        }

        public async Task<IEnumerable<GetTodoDto>> GetTodosByStatus(bool isDone = true)
        {
            var todos = await _context.Todos.Where(x => x.IsDone.Equals(isDone)).ToListAsync();
            var todosToReturn = new List<GetTodoDto>();
            foreach (var todo in todos)
            {
                todosToReturn.Add(new GetTodoDto
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Time = todo.Time,
                    IsDone = todo.IsDone
                });
            }
            return todosToReturn;
        }

        public async Task<GetTodoDto> UpdateTodo(UpdateTodoDto model)
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

            todoInDb.DateUpdated = DateTime.Now;
            _context.Todos.Attach(todoInDb);
            var res = await _context.SaveChangesAsync(CancellationToken.None);
            if (res > 0)
            {
                return new GetTodoDto()
                {
                    Id = todoInDb.Id,
                    Title = todoInDb.Title,
                    Description = todoInDb.Description,
                    Time = todoInDb.Time,
                    IsDone = todoInDb.IsDone
                };
            }
            return null;

        }
    }
}
