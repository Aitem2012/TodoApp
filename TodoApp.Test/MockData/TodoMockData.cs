using TodoApp.Application.Dto;
using TodoApp.Domain.Todos;

namespace TodoApp.Test.MockData
{
    public class TodoMockData
    {
        public static List<Todo> GetTodos()
        {
            return new List<Todo>
            {
                new Todo { Id = Guid.Parse("b0434f49-55a0-4fb5-b0c6-9d5971d4cd42"), Title = "Market", Description = "Get some groceries from the market", Time = DateTime.Now.AddHours(6), IsDone = false },
                new Todo { Id = Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee"), Title = "Dry cleaner", Description = "Pick up clothes from drycleaner", Time = DateTime.Now.AddHours(4), IsDone = false }

            };
        }

        public static List<Todo> NoContentTodos()
        {
            return new List<Todo>();
        }
        public static Todo GetTodo(Guid todoId)
        {
            return GetTodos().FirstOrDefault(x => x.Id.Equals(todoId));

        }

        public static bool DeleteTodo(Guid id)
        {
            var todo = GetTodo(id);
            var todos = GetTodos();
            todos.Remove(todo);

            return !todos.Contains(todo) && todo != null;
        }

        public static Todo UpdateTodo(Todo todo)
        {
            var todos = GetTodos().ToHashSet<Todo>();
            var theTodos = todos.FirstOrDefault(x => x.Id.Equals(todo.Id));
            if (theTodos != null)
            {
                todos.Add(todo);
            }

            return todos.Contains(todo) ? todo : null;

        }
        public static Todo CreateTodo(CreateTodoDto todo)
        {
            var todos = GetTodos();
            var newTodo = new Todo
            {
                Id = Guid.NewGuid(),
                Title = todo.Title,
                Description = todo.Description,
                Time = todo.Time,
                IsDone = todo.IsDone
            };
            todos.Add(newTodo);
            return todos.Contains(newTodo) ? newTodo : null;
        }
    }
}
