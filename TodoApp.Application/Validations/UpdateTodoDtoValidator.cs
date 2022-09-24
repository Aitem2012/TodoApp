using FluentValidation;
using TodoApp.Application.Dto;

namespace TodoApp.Application.Validations
{
    public class UpdateTodoDtoValidator : AbstractValidator<UpdateTodoDto>
    {
        public UpdateTodoDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.IsDone).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.Time).NotEmpty().NotNull().WithMessage("{propertyName} cannot be null or empty");
        }
    }
}
