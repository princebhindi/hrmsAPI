using FluentValidation;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Validators
{
    public class DepartMentDtoValidator : AbstractValidator<DepartMentDto>
    {
        public DepartMentDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department Name is required.")
                .MaximumLength(100).WithMessage("Department Name cannot exceed 100 characters.");
        }
    }
}
