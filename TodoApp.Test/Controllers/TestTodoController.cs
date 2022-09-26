using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TodoApp.Application.Abstract.Repositories;
using TodoApp.Application.Dto;
using TodoApp.Domain.Todos;
using TodoApp.Persistence.Context;
using TodoApp.Persistence.Repository;
using TodoApp.Test.Db;
using TodoApp.Test.MockData;
using TodoApp.Web.Controllers;

namespace TodoApp.Test.Controllers
{
    public class TestTodoController
    {
        private TodoRepository _repository;
        public static DbContextOptions<AppDbContext> dbContext { get; }
        public static string conn = "Data source=TodoAppTest";

        static TestTodoController()
        {
            dbContext = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(conn)
                .Options;
        }
        public TestTodoController()
        {
            var context = new AppDbContext(dbContext);
            DummyDbInitializer db = new DummyDbInitializer();
            db.Seed(context);

            _repository = new TodoRepository(context);
        }

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
            todoRepo.Setup(x => x.GetTodoById(Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee"))).ReturnsAsync(TodoMockData.GetTodo());
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
            todoRepo.Setup(x => x.GetTodoById(Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee"))).ReturnsAsync(TodoMockData.GetTodo());
            var sut = new TodosController(todoRepo.Object);

            //Act
            var result = await sut.GetTodoById(Guid.Empty);

            //Assert
            Assert.IsType<NotFoundResult>(result);
            Assert.True((result as NotFoundResult).StatusCode.Equals(404));
        }

        [Fact]
        public async void DeleteTodoById_ShouldReturn200StatusCode()
        {
            //Arrange
            var todoId = Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee");
            var controller = new TodosController(_repository);
            //Act
            var sut = await controller.DeleteTodo(todoId);

            Assert.IsType<OkObjectResult>(sut);
            Assert.True((sut as OkObjectResult).StatusCode.Equals(200));
        }

        [Fact]
        public async void DeleteTodoById_ShouldReturn400StatusCode()
        {
            //Arrange
            var todoId = Guid.Parse("8d48c1d1-cecc-4f0b-a1f6-4fbacca839ee");
            var controller = new TodosController(_repository);

            //Act
            var sut = await controller.DeleteTodo(todoId);

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
            var controller = new TodosController(_repository);

            //Act
            var sut = await controller.UpdateTodo(todo);

            //Assert
            Assert.IsType<OkObjectResult>(sut);
            Assert.True((sut as OkObjectResult).StatusCode.Equals(200));
        }
        [Fact]
        public async void UpdateTodo_Returns_Null_When_Invalid_Id_Is_Given()
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
            var controller = new TodosController(_repository);

            //Act
            var sut = await controller.UpdateTodo(todo);

            //Assert
            Assert.IsType<BadRequestResult>(sut);
            Assert.True((sut as BadRequestResult).StatusCode.Equals(400));
        }

        [Fact]
        public async void CreateTodo_ShouldReturn200StatusCode()
        {
            //Arrange
            var todo = new CreateTodoDto { Title = "Car", Description = "Wash the car", Time = DateTime.Now.AddHours(6), IsDone = false };
            var controller = new TodosController(_repository);
            //Act 

            var sut = await controller.CreateTodo(todo);

            ///Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);
            Assert.True((sut as OkObjectResult).StatusCode.Equals(200));
        }
        [Fact]
        public void CreateTodo_With_Invalid_Data_Throws_Exception()
        {
            //Arrange
            var todo = new CreateTodoDto { Title = "Car", Time = DateTime.Now.AddHours(6), IsDone = false };
            var controller = new TodosController(_repository);
            //Assert
            var sut = Assert.ThrowsAny<Exception>(() => controller.CreateTodo(todo).Result);
        }
    }
}
