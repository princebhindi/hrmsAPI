using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class AddDepartMentCommandValidator : AbstractValidator<AddDepartMentCommand>
    {
        public AddDepartMentCommandValidator()
        {
            RuleFor(x => x.DepartMent)
                .NotNull().WithMessage("Department data cannot be null.")
                .SetValidator(new DepartMentDtoValidator());
        }
    }
}
