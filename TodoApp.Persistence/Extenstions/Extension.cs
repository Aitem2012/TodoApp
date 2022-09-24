using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TodoApp.Application.Abstract.Persistence;
using TodoApp.Application.Abstract.Repositories;
using TodoApp.Application.Dto;
using TodoApp.Application.Profiles;
using TodoApp.Persistence.Context;
using TodoApp.Persistence.Repository;

namespace TodoApp.Persistence.Extenstions
{
    public static class Extension
    {
        public static void AddDatabaseService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(config.GetConnectionString("DefaultConnection")));
        }
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<ITodoRepository, TodoRepository>();

            services.AddFluentValidation(opt => opt.RegisterValidatorsFromAssembly(typeof(CreateTodoDto).GetTypeInfo().Assembly));
            services.AddAutoMapper(typeof(TodoAppMappingProfile));
        }
    }
}
