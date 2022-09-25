using TodoApp.Persistence.Context;

namespace TodoApp.Test.Db
{
    public class DummyDbInitializer
    {
        public DummyDbInitializer()
        {

        }
        public void Seed(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Todos.AddRange(
                new Domain.Todos.Todo { Id = Guid.Parse("b0434f49-55a0-4fb5-b0c6-9d5971d4cd42"), Title = "Market", Description = "Get some groceries from the market", Time = DateTime.Now.AddHours(6), IsDone = false, DateCreated = DateTime.Now, DateUpdated = DateTime.Now },
                new Domain.Todos.Todo { Id = Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee"), Title = "Dry cleaner", Description = "Pick up clothes from drycleaner", Time = DateTime.Now.AddHours(4), IsDone = false, DateCreated = DateTime.Now, DateUpdated = DateTime.Now }
                                    );
            context.SaveChangesAsync();
        }
    }
}
