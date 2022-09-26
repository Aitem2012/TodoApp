using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApp.Application.Abstract.Repositories;
using TodoApp.Application.Dto;
using TodoApp.Domain.Todos;
using TodoApp.Test.MockData;
using TodoApp.Web.Controllers;

namespace TodoApp.Test.Controllers
{
    public class TestTodoController
    {

        [Fact]
        public async void GetAllTodos_ShouldReturn200StatusCode()
        {
            //Arrange
            var todoRepo = new Mock<ITodoRepository>();
            todoRepo.Setup(x => x.GetAllTodos()).ReturnsAsync(TodoMockData.GetTodos());
            var sut = new TodosController(todoRepo.Object);

            //Act
            var result = await sut.GetTodos();

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.True((result as OkObjectResult).StatusCode.Equals(200));
        }
        [Fact]
        public async void GetAllTodos_ShouldReturn204StatusCode()
        {
            //Arrange
            var todoRepo = new Mock<ITodoRepository>();
            todoRepo.Setup(x => x.GetAllTodos()).ReturnsAsync(TodoMockData.NoContentTodos());
            var sut = new TodosController(todoRepo.Object);

            //Act
            var result = await sut.GetTodos();

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.True((result as NoContentResult).StatusCode.Equals(204));
        }

        [Fact]
        public async void GetTodoById_ShouldReturn200StatusCode()
        {
            //Arrange
            var todoRepo = new Mock<ITodoRepository>();
            todoRepo.Setup(x => x.GetTodoById(Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee"))).ReturnsAsync(TodoMockData.GetTodo(Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee")));
            var sut = new TodosController(todoRepo.Object);

            //Act
            var result = await sut.GetTodoById(Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee"));

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.True((result as OkObjectResult).StatusCode.Equals(200));
        }

        [Fact]
        public async void GetTodoById_ShouldReturn404StatusCode()
        {
            //Arrange
            var todoRepo = new Mock<ITodoRepository>();
            todoRepo.Setup(x => x.GetTodoById(Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee"))).ReturnsAsync(TodoMockData.GetTodo(Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee")));
            var sut = new TodosController(todoRepo.Object);

            //Act
            var result = await sut.GetTodoById(Guid.Empty);

            //Assert
            Assert.IsType<NotFoundResult>(result);
            Assert.True((result as NotFoundResult).StatusCode.Equals(404));
        }

        [Fact]
        public async void DeleteTodoById_ShouldReturn204StatusCode()
        {
            //Arrange
            var todoRepo = new Mock<ITodoRepository>();
            todoRepo.Setup(x => x.DeleteTodo(Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee")).Result).Returns(TodoMockData.DeleteTodo(Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee")));

            var res = new TodosController(todoRepo.Object);

            //Act
            var sut = await res.DeleteTodo(Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee"));

            Assert.IsType<NoContentResult>(sut);
            Assert.True((sut as NoContentResult).StatusCode.Equals(204));
            Assert.IsAssignableFrom<NoContentResult>(sut);
        }

        [Fact]
        public async void DeleteTodoById_ShouldReturn400StatusCode()
        {
            //Arrange
            var todoId = Guid.Parse("8d48c1d1-cecc-4f0b-a1f6-4fbacca839ee");
            var todoRepo = new Mock<ITodoRepository>();
            todoRepo.Setup(x => x.DeleteTodo(todoId).Result).Returns(TodoMockData.DeleteTodo(todoId));

            var res = new TodosController(todoRepo.Object);

            //Act
            var sut = await res.DeleteTodo(todoId);



            //Assert
            Assert.IsType<BadRequestResult>(sut);
            Assert.True((sut as BadRequestResult).StatusCode.Equals(400));
        }

        [Fact]
        public async void UpdateTodo_ShouldReturn200StatusCode()
        {
            //Arrange
            var todo = new Todo
            {
                Id = Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee"),
                Title = "This is a new title",
                Description = "This is a new description",
                IsDone = true,
                Time = DateTime.Now.AddMinutes(25),
            };
            var todoRepo = new Mock<ITodoRepository>();
            todoRepo.Setup(x => x.UpdateTodo(todo).Result).Returns(TodoMockData.UpdateTodo(todo));

            var res = new TodosController(todoRepo.Object);
            //Act
            var sut = await res.UpdateTodo(todo);

            //Assert
            Assert.IsType<OkObjectResult>(sut);
            Assert.True((sut as OkObjectResult).StatusCode.Equals(200));
        }
        [Fact]
        public async void UpdateTodo_ShouldReturns400StatusCode()
        {
            //Arrange
            var todo = new Todo
            {
                Id = Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839e1"),
                Title = "This is a new title",
                Description = "This is a new description",
                IsDone = true,
                Time = DateTime.Now.AddMinutes(25),
            };
            var todoRepo = new Mock<ITodoRepository>();
            todoRepo.Setup(x => x.UpdateTodo(todo).Result).Returns(TodoMockData.UpdateTodo(todo));

            var res = new TodosController(todoRepo.Object);
            //Act
            var sut = await res.UpdateTodo(todo);

            //Assert
            Assert.IsType<BadRequestResult>(sut);
            Assert.True((sut as BadRequestResult).StatusCode.Equals(400));
        }

        [Fact]
        public async void CreateTodo_ShouldReturn200StatusCode()
        {
            //Arrange
            var todo = new CreateTodoDto { Title = "Car", Description = "Wash the car", Time = DateTime.Now.AddHours(6), IsDone = false };
            var todoRepo = new Mock<ITodoRepository>();
            todoRepo.Setup(x => x.CreateTodo(todo).Result).Returns(TodoMockData.CreateTodo(todo));
            var res = new TodosController(todoRepo.Object);

            //Act 

            var sut = await res.CreateTodo(todo);

            ///Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);
            Assert.True((sut as OkObjectResult).StatusCode.Equals(200));
        }

    }
}
