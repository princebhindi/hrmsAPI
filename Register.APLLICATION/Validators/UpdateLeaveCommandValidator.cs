using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class UpdateLeaveCommandValidator : AbstractValidator<UpdateLeaveCommand>
    {
        public UpdateLeaveCommandValidator()
        {
            RuleFor(x => x.Leave)
                .NotNull().WithMessage("Leave data cannot be null.")
                .SetValidator(new LeavesDtoValidator());
        }
    }
}
