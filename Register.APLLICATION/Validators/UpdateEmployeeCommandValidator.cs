using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Employee)
                .NotNull().WithMessage("Employee data cannot be null.")
                .SetValidator(new EmployeeDtoValidator());
        }
    }
}
