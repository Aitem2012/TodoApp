using Moq;
using TodoApp.Application.Abstract.Repositories;
using TodoApp.Application.Dto;
using TodoApp.Web.Controllers;

namespace TodoApp.Test
{
    public class GetTodosTests
    {
        [Fact]
        public async void GetTodoById()
        {
            var todoRepo = new Mock<ITodoRepository>();
            var todoController = new TodosController(todoRepo.Object);

            todoRepo.Setup(x => x.CreateTodo(new CreateTodoDto { Description = "This is a description", IsDone = true, Time = DateTime.Now.AddHours(6), Title = "The Title" }).IsCompleted);
            var todoId = Guid.Parse("F68C59EB-BF6B-47C4-A7F5-2632CC1A3199");

            var res = await todoController.GetTodos();
            Assert.NotNull(res);
        }
    }
}



