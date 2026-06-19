using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class AddLeaveCommandValidator : AbstractValidator<AddLeaveCommand>
    {
        public AddLeaveCommandValidator()
        {
            RuleFor(x => x.Leave)
                .NotNull().WithMessage("Leave data cannot be null.")
                .SetValidator(new LeavesDtoValidator());
        }
    }
}
