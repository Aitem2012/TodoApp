using AutoMapper;
using TodoApp.Application.Dto;
using TodoApp.Domain.Todos;

namespace TodoApp.Application.Profiles
{
    public class TodoAppMappingProfile : Profile
    {
        public TodoAppMappingProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<CreateTodoDto, Todo>();
            CreateMap<UpdateTodoDto, Todo>();
            CreateMap<Todo, GetTodoDto>();
        }
    }
}
