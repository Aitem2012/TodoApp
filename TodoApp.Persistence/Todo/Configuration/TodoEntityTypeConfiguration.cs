using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApp.Persistence.Todo.Configuration
{
    public class TodoEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Todos.Todo>
    {
        public void Configure(EntityTypeBuilder<Domain.Todos.Todo> builder)
        {
            builder.ToTable("Todos");
            builder.HasKey(x => x.Id);
        }
    }
}
