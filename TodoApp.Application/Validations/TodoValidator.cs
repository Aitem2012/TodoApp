using FluentValidation;
using TodoApp.Domain.Todos;

namespace TodoApp.Application.Validations
{
    public class TodoValidator : AbstractValidator<Todo>
    {
        public TodoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.IsDone).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.Time).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
        }
    }
}
