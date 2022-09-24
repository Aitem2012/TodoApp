using FluentValidation;
using TodoApp.Application.Dto;

namespace TodoApp.Application.Validations
{
    public class CreateTodoDtoValidator : AbstractValidator<CreateTodoDto>
    {
        public CreateTodoDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.IsDone).NotNull().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.Time).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
        }
    }
}
