using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class AddEmployeeCommandValidator : AbstractValidator<AddEmployeeCommand>
    {
        public AddEmployeeCommandValidator()
        {
            RuleFor(x => x.Employee)
                .NotNull().WithMessage("Employee data cannot be null.")
                .SetValidator(new EmployeeDtoValidator());
        }
    }
}
