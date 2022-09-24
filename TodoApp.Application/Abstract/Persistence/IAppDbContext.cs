using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Todos;

namespace TodoApp.Application.Abstract.Persistence
{
    public interface IAppDbContext
    {
        public DbSet<Todo> Todos { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
