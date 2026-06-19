using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class UpdateDepartMentCommandValidator : AbstractValidator<UpdateDepartMentCommand>
    {
        public UpdateDepartMentCommandValidator()
        {
            RuleFor(x => x.DepartMent)
                .NotNull().WithMessage("Department data cannot be null.")
                .SetValidator(new DepartMentDtoValidator());
        }
    }
}
