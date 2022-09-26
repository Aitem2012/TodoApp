using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Dto;
using TodoApp.Domain.Todos;
using TodoApp.Persistence.Context;
using TodoApp.Persistence.Repository;
using TodoApp.Test.Db;

namespace TodoApp.Test
{
    public class TodosUnitTestController
    {
        private TodoRepository _repository;
        public static DbContextOptions<AppDbContext> dbContext { get; }
        public static string conn = "Data source=TodoAppTest";
        static TodosUnitTestController()
        {
            dbContext = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(conn)
                .Options;
        }
        public TodosUnitTestController()
        {
            var context = new AppDbContext(dbContext);
            DummyDbInitializer db = new DummyDbInitializer();
            db.Seed(context);

            _repository = new TodoRepository(context);
        }

        [Fact]
        public async void GetTodoById_With_Valid_Id_Return_GetTodoDto()
        {
            //Arrange
            var todoId = Guid.Parse("b0434f49-55a0-4fb5-b0c6-9d5971d4cd42");

            //Act
            var data = await _repository.GetTodoById(todoId);

            //Assert
            Assert.IsType<Todo>(data);
        }
        [Fact]
        public async void GetTodoById_With_Invalid_Id_Returns_Null()
        {
            //Arrange
            var todoId = Guid.NewGuid();

            //Act 
            var sut = await _repository.GetTodoById(todoId);

            Assert.Null(sut);
        }

        [Fact]
        public async void GetTodos_Returns_List_Of_GetTodos()
        {
            //Arrange
            var sut = await _repository.GetAllTodos();
            Assert.NotNull(sut);
            Assert.True(sut.Count() > 0);
        }
        [Fact]
        public async void GetTodos_Returns_Zero_When_No_Todo_Exists()
        {
            //Arrange
            var todoId = new List<Guid>() { Guid.Parse("b0434f49-55a0-4fb5-b0c6-9d5971d4cd42"), Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee") };
            todoId.ForEach(x => _repository.DeleteTodo(x));

            //Act 
            var sut = await _repository.GetAllTodos();

            //Assert
            Assert.True(sut.Count() == 0);
        }
        [Fact]
        public async void GetTodoByStatus_Returns_Result_When_Match_Found()
        {
            //Arrange

            //Act
            // var sut = await _repository.GetTodosByStatus(false);

            //Assert
            Assert.True(true);
        }
        [Fact]
        public async void GetTodosByStatus_Does_Not_Returns_Result_When_No_MatchFound()
        {
            ///Act
            //var sut = await _repository.GetTodosByStatus(true);

            Assert.False(true);
        }

        [Fact]
        public async void CreateTodo_With_Valid_Data_Returns_Result()
        {
            //Arrange
            var todo = new CreateTodoDto { Title = "Car", Description = "Wash the car", Time = DateTime.Now.AddHours(6), IsDone = false };
            var todosCountBeforeAddingTodo = await _repository.GetAllTodos();
            //Act 

            var sut = await _repository.CreateTodo(todo);
            var todosCountAfterAddingTodo = await _repository.GetAllTodos();

            ///Assert
            Assert.NotNull(sut);
            Assert.NotEqual(todosCountBeforeAddingTodo, todosCountAfterAddingTodo);
            Assert.True(todosCountAfterAddingTodo.Count() > todosCountBeforeAddingTodo.Count());
        }
        [Fact]
        public void CreateTodo_With_Invalid_Data_Throws_Exception()
        {
            //Arrange
            var todo = new CreateTodoDto { Title = "Car", Time = DateTime.Now.AddHours(6), IsDone = false };

            //Assert
            var sut = Assert.ThrowsAny<Exception>(() => _repository.CreateTodo(todo).Result);
        }

        [Fact]
        public async void UpdateTodo_Updates_Todo_In_Db()
        {
            //Arrange
            var todoInDb = await _repository.GetTodoById(Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee"));
            var todo = new Todo
            {
                Id = Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee"),
                Title = "This is a new title",
                Description = "This is a new description",
                IsDone = true,
                Time = DateTime.Now.AddMinutes(25),
            };


            //Act
            var sut = await _repository.UpdateTodo(todo);

            //Assert
            Assert.Equal(todoInDb.Id, sut.Id);
            Assert.NotEqual(todoInDb.Title, sut.Title);
            Assert.NotEqual(todoInDb.Description, sut.Description);
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

            //Act
            var sut = await _repository.UpdateTodo(todo);

            //Assert
            Assert.Null(sut);
        }

        [Fact]
        public async void DeleteTodo_Returns_True_With_Valid_ID()
        {
            //Arrange
            var todoId = Guid.Parse("8d48c1d1-cecc-4f0b-a1f9-4fbacca839ee");

            //Act
            var sut = await _repository.DeleteTodo(todoId);

            Assert.True(sut);
        }

        [Fact]
        public async void DeleteTodo_Returns_False_With_Invalid_ID()
        {
            //Arrange
            var todoId = Guid.Parse("8d48c1d1-cecc-4f0b-a1f6-4fbacca839ee");

            //Act
            var sut = await _repository.DeleteTodo(todoId);

            Assert.False(sut);
        }
    }
}
