using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class AddEmployeeDocumentCommandValidator : AbstractValidator<AddEmployeeDocumentCommand>
    {
        public AddEmployeeDocumentCommandValidator()
        {
            RuleFor(x => x.EmployeeDocument)
                .NotNull().WithMessage("Employee Document data cannot be null.")
                .SetValidator(new EmployeeDocumentDtoValidator());
        }
    }
}
