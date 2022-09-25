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
            var todoDto = new CreateTodoDto
            {
                Time = DateTime.Now.AddHours(6),
                Title = "Some title",
                Description = "Some description",
                IsDone = false,
            };

            todoRepo.Setup(x => x.CreateTodo(todoDto).Result).Returns(new GetTodoDto());


            var res = todoController.GetTodos().Result;

            Assert.NotNull(res);
        }


    }
}



